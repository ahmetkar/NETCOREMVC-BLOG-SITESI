'use client';
import { Form, Input, Button, Card, App } from 'antd';
import { UserOutlined, LockOutlined } from '@ant-design/icons';
import { useState, useEffect } from 'react';
import { fetchGenelVeriler } from '@/services/visitor/generalService';
import { yoneticiGiris } from '@/services/admin/authService';
import { getImageUrl } from '@/lib/image-url';
import Cookies from 'js-cookie';
import { useRouter } from 'next/navigation';

export default function AdminLogin() {
  const { message } = App.useApp();
  const [loading, setLoading] = useState(false);
  const [adminLogo, setAdminLogo] = useState<string | null>(null);
  const router = useRouter();

  async function fetchLogo() {
    try {
      const res = await fetchGenelVeriler();
      if (res?.settings?.logoImage?.fileName) {
        setAdminLogo(res.settings.logoImage.fileName);
      }
    } catch (error) {
      console.error("Logo fetch error:", error);
    }
  }

  useEffect(() => {
    fetchLogo();
  }, []);

  const onFinish = async (values: any) => {
    setLoading(true);
    try {
      const response = await yoneticiGiris(values);
      const { token, expiration } = response;

      // Save token in cookie with security flags
      Cookies.set('token', token, {
        expires: new Date(expiration),
        path: '/',
        sameSite: 'lax',
        secure: typeof window !== 'undefined' && window.location.protocol === 'https:',
      });

      message.success('Giriş başarılı, yönlendiriliyorsunuz...');
      // Decode token to find role
      try {
        const base64Url = token.split('.')[1];
        let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        while (base64.length % 4) {
          base64 += '=';
        }
        const jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
          return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));
        const payload = JSON.parse(jsonPayload);
        const role = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || payload.role;

        if (role === 'Superadmin' || role === 'Editor') {
          router.push('/admin');
        } else {
          router.push('/admin/articles');
        }
      } catch (e) {
        router.push('/admin/articles');
      }
    } catch (error: any) {
      const errorMsg = error.response?.data?.message || 'Giriş başarısız. Lütfen bilgilerinizi kontrol edin.';
      message.error(errorMsg);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen flex flex-col items-center justify-center bg-gray-100">
      {adminLogo && (
        <div className="mb-8">
          <img src={getImageUrl(adminLogo)} alt="Admin Logo" className="h-18 w-auto object-contain" />
        </div>
      )}
      <Card title="Admin Paneli Girişi" variant="borderless" style={{ width: 400, boxShadow: '0 4px 12px rgba(0,0,0,0.1)' }}>
        <Form
          name="admin_login"
          initialValues={{ remember: true }}
          onFinish={onFinish}
          layout="vertical"
        >
          <Form.Item
            name="email"
            rules={[
              { required: true, message: 'Lütfen email adresinizi girin!' },
              { type: 'email', message: 'Geçerli bir email adresi girin!' }
            ]}
          >
            <Input prefix={<UserOutlined />} placeholder="Email" size="large" />
          </Form.Item>

          <Form.Item
            name="password"
            rules={[{ required: true, message: 'Lütfen şifrenizi girin!' }]}
          >
            <Input.Password prefix={<LockOutlined />} placeholder="Şifre" size="large" />
          </Form.Item>

          <Form.Item>
            <Button type="primary" htmlType="submit" size="large" block loading={loading}>
              Giriş Yap
            </Button>
          </Form.Item>
        </Form>
      </Card>
    </div>
  );
}
