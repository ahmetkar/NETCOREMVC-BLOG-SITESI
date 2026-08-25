'use client';
import React, { useState, useEffect } from 'react';
import { Layout, Menu, Button, theme, Dropdown, App } from 'antd';
import {
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  DashboardOutlined,
  FileTextOutlined,
  AppstoreOutlined,
  UserOutlined,
  PictureOutlined,
  SettingOutlined,
  MessageOutlined,
  MailOutlined,
  LogoutOutlined,
  GlobalOutlined
} from '@ant-design/icons';
import { useRouter, usePathname } from 'next/navigation';
import Cookies from 'js-cookie';
import Link from 'next/link';
import { fetchAdminAyarlar } from '@/services/admin/settingSubscriberService';
import { getImageUrl } from '@/lib/image-url';

const { Header, Sider, Content } = Layout;

function decodeJWT(token: string) {
  try {
    const base64Url = token.split('.')[1];
    let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    while (base64.length % 4) {
      base64 += '=';
    }
    const jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
      return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
    return JSON.parse(jsonPayload);
  } catch (e) {
    console.error("JWT Decode error:", e);
    return null;
  }
}

export default function AdminLayout({ children }: { children: React.ReactNode }) {
  const [collapsed, setCollapsed] = useState(false);
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();
  const router = useRouter();
  const pathname = usePathname();
  const [adminLogo, setAdminLogo] = useState<string | null>(null);
  const [userPermissions, setUserPermissions] = useState<string[]>([]);
  const [userName, setUserName] = useState<string>('User');
  const [userRole, setUserRole] = useState<string>('');

  async function fetchSettings() {
    try {
      const res = await fetchAdminAyarlar();
      if (res?.adminPanelLogo?.fileName) {
        setAdminLogo(res.adminPanelLogo.fileName);
      }
    } catch (error) {
      console.error("Settings fetch error:", error);
    }
  }

  // Authentication check
  useEffect(() => {
    const token = Cookies.get('token');
    if (!token) {
      setUserPermissions([]);
      setUserName('User');
      setUserRole('');
      if (pathname !== '/admin/login') {
        router.push('/admin/login');
      }
    } else {
      const payload = decodeJWT(token);
      console.log("Decoded JWT Payload:", payload);
      if (payload) {
        const perms = payload.Permission || payload.permission || [];
        const permsArray = Array.isArray(perms) ? perms : [perms];
        console.log("Parsed Permissions:", permsArray);
        setUserPermissions(permsArray);

        const nameClaim = payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || payload.name;
        if (nameClaim) {
          setUserName(nameClaim);
        }

        const roleClaim = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || payload.role;
        if (roleClaim) {
          setUserRole(roleClaim);

          // Redirect non-admin/editor users away from dashboard
          if (pathname === '/admin' && roleClaim !== 'Superadmin' && roleClaim !== 'Editor') {
            router.push('/admin/articles');
          }
        }

        if (permsArray.includes('Settings.View')) {
          fetchSettings();
        }
      }
    }
  }, [pathname, router]);

  const handleLogout = () => {
    Cookies.remove('token', { path: '/' });
    Cookies.remove('token'); // Fallback for default path
    setUserPermissions([]);
    setUserName('User');
    setUserRole('');
    router.push('/admin/login');
  };

  const menuItems = [
    (userRole === 'Superadmin' || userRole === 'Editor' ? { key: '/admin', icon: <DashboardOutlined />, label: 'Dashboard' } : null),
    { key: '/admin/articles', icon: <FileTextOutlined />, label: 'Makaleler' },
    (userPermissions.includes('Categories.View') ? { key: '/admin/categories', icon: <AppstoreOutlined />, label: 'Kategoriler' } : null),
    (userPermissions.includes('Users.View') ? { key: '/admin/users', icon: <UserOutlined />, label: 'Kullanıcılar' } : null),
    (userRole === 'Superadmin' || userRole === 'Editor' ? { key: '/admin/media', icon: <PictureOutlined />, label: 'Medya' } : null),
    (userRole === 'Superadmin' || userRole === 'Editor' ? { key: '/admin/messages', icon: <MessageOutlined />, label: 'Mesajlar' } : null),
    (userRole === 'Superadmin' || userRole === 'Editor' ? { key: '/admin/subscribers', icon: <MailOutlined />, label: 'Aboneler' } : null),
    (userPermissions.includes('Settings.View') ? { key: '/admin/settings', icon: <SettingOutlined />, label: 'Ayarlar' } : null),
  ].filter(Boolean) as any;

  const userMenu: import('antd').MenuProps = {
    items: [
      { key: 'profile', icon: <UserOutlined />, label: <Link href="/admin/profile">Profilim</Link> },
      { type: 'divider' },
      { key: 'logout', icon: <LogoutOutlined />, label: 'Çıkış Yap', onClick: handleLogout },
    ]
  };

  if (pathname === '/admin/login') {
    return <App>{children}</App>;
  }

  return (
    <App>
      <Layout style={{ minHeight: '100vh' }}>
        <Sider
          trigger={null}
          collapsible
          collapsed={collapsed}
          theme="dark"
          breakpoint="lg"
          collapsedWidth="0"
          onBreakpoint={(broken) => {
            if (broken) setCollapsed(true);
          }}
        >
          <div className="h-9 flex items-center justify-center text-white text-xl font-bold bg-gray-900">
            {adminLogo && !collapsed ? (
              <img src={getImageUrl(adminLogo)} alt="Admin Logo" className="h-7 w-auto object-contain transition-transform duration-300" />
            ) : (
              collapsed ? 'B.' : 'Blog Admin'
            )}
          </div>
          <Menu
            theme="dark"
            mode="inline"
            selectedKeys={[pathname]}
            items={menuItems}
            onClick={(e) => router.push(e.key)}
          />
        </Sider>
        <Layout>
          <Header style={{ padding: '0 24px', background: colorBgContainer, display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <Button
              type="text"
              icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
              onClick={() => setCollapsed(!collapsed)}
              style={{ fontSize: '16px', width: 64, height: 64, marginLeft: -24 }}
            />
            <div className="flex items-center gap-4">
              <Button type="link" icon={<GlobalOutlined />} onClick={() => router.push('/')}>
                Siteye Dön
              </Button>
              <Dropdown menu={userMenu} placement="bottomRight">
                <Button type="text" icon={<UserOutlined />}>
                  {userName} {userRole ? `(${userRole})` : ''}
                </Button>
              </Dropdown>
            </div>
          </Header>
          <Content
            style={{
              margin: '24px 16px',
              padding: 24,
              minHeight: 280,
              background: colorBgContainer,
              borderRadius: borderRadiusLG,
              overflow: 'auto'
            }}
          >
            {children}
          </Content>
        </Layout>
      </Layout>
    </App>
  );
}
