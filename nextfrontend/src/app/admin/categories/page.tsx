'use client';
import { useEffect, useState } from 'react';
import { Table, Button, Space, Popconfirm, Tag, Tooltip, Modal, Form, Input, App } from 'antd';
import { EditOutlined, DeleteOutlined, PlusOutlined, RollbackOutlined } from '@ant-design/icons';
import { fetchAdminKategoriler, deleteAdminKategori, createAdminKategori, updateAdminKategori } from '@/services/admin/categoryService';
import { CategoryDto } from '@/types/dto';
import Link from 'next/link';

export default function AdminCategories() {
  const { message } = App.useApp();
  const [categories, setCategories] = useState<CategoryDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [modalMode, setModalMode] = useState<'add' | 'edit'>('add');
  const [editingCategoryId, setEditingCategoryId] = useState<string | null>(null);
  const [form] = Form.useForm();

  async function fetchCategories() {
    setLoading(true);
    try {
      const data = await fetchAdminKategoriler();
      setCategories(data);
    } catch (error) {
      console.error(error);
      message.error('Kategoriler yüklenirken hata oluştu.');
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    fetchCategories();
  }, []);

  async function handleDelete(id: string) {
    try {
      const res = await deleteAdminKategori(id);
      message.success(res.message || 'Kategori çöp kutusuna taşındı.');
      fetchCategories();
    } catch (error: any) {
      message.error(error.response?.data?.message || 'Kategori silinirken hata oluştu.');
    }
  }

  const openAddModal = () => {
    setModalMode('add');
    form.resetFields();
    setIsModalVisible(true);
  };

  const openEditModal = (record: CategoryDto) => {
    setModalMode('edit');
    setEditingCategoryId(record.id || null);
    form.setFieldsValue({
      name: record.name,
    });
    setIsModalVisible(true);
  };

  async function handleModalSubmit() {
    try {
      const values = await form.validateFields();
      if (modalMode === 'add') {
        const res = await createAdminKategori(values);
        message.success(res.message || 'Kategori başarıyla eklendi.');
      } else {
        const res = await updateAdminKategori(editingCategoryId || '', values);
        message.success(res.message || 'Kategori başarıyla güncellendi.');
      }
      setIsModalVisible(false);
      fetchCategories();
    } catch (error: any) {
      if(error.response?.data?.message) {
         message.error(error.response.data.message);
      }
    }
  }

  const columns = [
    {
      title: 'Kategori Adı',
      dataIndex: 'name',
      key: 'name',
      render: (text: string) => <Tag color="blue">{text}</Tag>,
    },
    {
      title: 'İşlemler',
      key: 'action',
      render: (_: any, record: CategoryDto) => (
        <Space size="middle">
          <Tooltip title="Düzenle">
            <Button type="primary" icon={<EditOutlined />} onClick={() => openEditModal(record)} />
          </Tooltip>
          <Popconfirm
            title="Emin misiniz?"
            description="Kategori çöp kutusuna taşınacaktır."
            onConfirm={() => handleDelete(record.id || '')}
            okText="Evet"
            cancelText="Hayır"
          >
            <Tooltip title="Çöp Kutusuna Taşı">
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
        <h1 className="text-2xl font-bold">Kategori Yönetimi</h1>
        <div className="space-x-4">
          <Link href="/admin/categories/deleted">
            <Button type="default" icon={<RollbackOutlined />}>
              Çöp Kutusu
            </Button>
          </Link>
          <Button type="primary" icon={<PlusOutlined />} onClick={openAddModal}>
            Yeni Kategori Ekle
          </Button>
        </div>
      </div>

      <Table 
        columns={columns} 
        dataSource={categories} 
        rowKey="id" 
        loading={loading}
        pagination={{ defaultPageSize: 10, showSizeChanger: true }}
      />

      <Modal
        title={modalMode === 'add' ? 'Yeni Kategori Ekle' : 'Kategori Düzenle'}
        open={isModalVisible}
        onOk={handleModalSubmit}
        onCancel={() => setIsModalVisible(false)}
        okText="Kaydet"
        cancelText="İptal"
      >
        <Form form={form} layout="vertical">
          <Form.Item name="name" label="Kategori Adı" rules={[{ required: true, message: 'Kategori adı zorunludur' }]}>
            <Input placeholder="Örn: Teknoloji" />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
}

