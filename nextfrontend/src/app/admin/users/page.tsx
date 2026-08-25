'use client';
import { useEffect, useState } from 'react';
import { App,  Table, Button, Space, Popconfirm, Tag, Tooltip, Modal, Form, Input, Select  } from 'antd';
import { EditOutlined, DeleteOutlined, PlusOutlined } from '@ant-design/icons';
import { fetchAdminKullanicilar, fetchAdminRoller, createAdminKullanici, updateAdminKullanici, deleteAdminKullanici } from '@/services/admin/userService';
import { UserDto } from '@/types/dto';

export default function AdminUsers() {
  const { message } = App.useApp();
  const [users, setUsers] = useState<UserDto[]>([]);
  const [roles, setRoles] = useState<{ id: string; name: string }[]>([]);
  const [loading, setLoading] = useState(true);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [modalMode, setModalMode] = useState<'add' | 'edit'>('add');
  const [form] = Form.useForm();
  const [editingUserId, setEditingUserId] = useState<string | null>(null);

  useEffect(() => {
    fetchUsers();
    fetchRoles();
  }, []);

  async function fetchUsers() {
    setLoading(true);
    try {
      const data = await fetchAdminKullanicilar();
      setUsers(data);
    } catch (error) {
      console.error(error);
      message.error('Kullanıcılar yüklenirken hata oluştu.');
    } finally {
      setLoading(false);
    }
  }

  async function fetchRoles() {
    try {
      const data = await fetchAdminRoller();
      setRoles(data);
    } catch (error) {
      console.error(error);
    }
  }

  const handleDelete = async (id: string) => {
    try {
      const res = await deleteAdminKullanici(id);
      message.success(res.message || 'Kullanıcı başarıyla silindi.');
      fetchUsers();
    } catch (error: any) {
      message.error(error.response?.data?.message || 'Kullanıcı silinirken hata oluştu.');
    }
  };

  const openAddModal = () => {
    setModalMode('add');
    form.resetFields();
    setIsModalVisible(true);
  };

  const openEditModal = (record: UserDto & any) => {
    setModalMode('edit');
    setEditingUserId(record.id || null);
    form.setFieldsValue({
      firstName: record.firstName,
      lastName: record.lastName,
      email: record.email,
      phoneNumber: record.phoneNumber,
      roleId: record.roleId
    });
    setIsModalVisible(true);
  };

  const handleModalSubmit = async () => {
    try {
      const values = await form.validateFields();
      if (modalMode === 'add') {
        const res = await createAdminKullanici(values);
        message.success(res.message || 'Kullanıcı başarıyla eklendi.');
      } else {
        const res = await updateAdminKullanici({ id: editingUserId, ...values });
        message.success(res.message || 'Kullanıcı başarıyla güncellendi.');
      }
      setIsModalVisible(false);
      fetchUsers();
    } catch (error: any) {
      if(error.response?.data?.message) {
         message.error(error.response.data.message);
      }
    }
  };

  const columns = [
    {
      title: 'Ad',
      dataIndex: 'firstName',
      key: 'firstName',
    },
    {
      title: 'Soyad',
      dataIndex: 'lastName',
      key: 'lastName',
    },
    {
      title: 'E-posta',
      dataIndex: 'email',
      key: 'email',
    },
    {
      title: 'Telefon',
      dataIndex: 'phoneNumber',
      key: 'phoneNumber',
    },
    {
      title: 'Rol',
      dataIndex: 'role',
      key: 'role',
      render: (role: string) => <Tag color="geekblue">{role}</Tag>,
    },
    {
      title: 'İşlemler',
      key: 'action',
      render: (_: any, record: UserDto) => (
        <Space size="middle">
          <Tooltip title="Düzenle">
            <Button type="primary" icon={<EditOutlined />} onClick={() => openEditModal(record)} />
          </Tooltip>
          <Popconfirm
            title="Emin misiniz?"
            description="Kullanıcı kalıcı olarak silinecektir."
            onConfirm={() => handleDelete(record.id || '')}
            okText="Evet"
            cancelText="Hayır"
          >
            <Tooltip title="Sil">
              <Button danger icon={<DeleteOutlined />} />
            </Tooltip>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return (
    <div>
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Kullanıcı Yönetimi</h1>
        <Button type="primary" icon={<PlusOutlined />} onClick={openAddModal}>
          Yeni Kullanıcı Ekle
        </Button>
      </div>

      <Table 
        columns={columns} 
        dataSource={users} 
        rowKey="id" 
        loading={loading}
        pagination={{ defaultPageSize: 10, showSizeChanger: true }}
      />

      <Modal
        title={modalMode === 'add' ? 'Yeni Kullanıcı Ekle' : 'Kullanıcı Düzenle'}
        open={isModalVisible}
        onOk={handleModalSubmit}
        onCancel={() => setIsModalVisible(false)}
        okText="Kaydet"
        cancelText="İptal"
      >
        <Form form={form} layout="vertical">
          <Form.Item name="firstName" label="Ad" rules={[{ required: true, message: 'Lütfen ad giriniz' }]}>
            <Input />
          </Form.Item>
          <Form.Item name="lastName" label="Soyad" rules={[{ required: true, message: 'Lütfen soyad giriniz' }]}>
            <Input />
          </Form.Item>
          <Form.Item name="email" label="E-posta" rules={[{ required: true, type: 'email', message: 'Lütfen geçerli bir e-posta giriniz' }]}>
            <Input />
          </Form.Item>
          <Form.Item name="phoneNumber" label="Telefon">
            <Input />
          </Form.Item>
          {modalMode === 'add' && (
            <Form.Item name="password" label="Şifre" rules={[{ required: true, message: 'Lütfen şifre giriniz' }]}>
              <Input.Password />
            </Form.Item>
          )}
          <Form.Item name="roleId" label="Kullanıcı Rolü" rules={[{ required: true, message: 'Lütfen rol seçiniz' }]}>
            <Select placeholder="Rol seçin">
              {roles.map((role: any) => (
                <Select.Option key={role.id} value={role.id}>{role.name}</Select.Option>
              ))}
            </Select>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
}
