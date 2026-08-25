'use client';
import { useEffect, useState } from 'react';
import { App,  Table, Button, Space, Popconfirm, Tag, Tooltip  } from 'antd';
import { DeleteOutlined, RollbackOutlined, UndoOutlined } from '@ant-design/icons';
import { fetchAdminCopKategoriler, undoDeleteAdminKategori, forceDeleteAdminKategori } from '@/services/admin/categoryService';
import { CategoryDto } from '@/types/dto';
import Link from 'next/link';
import dayjs from 'dayjs';

export default function AdminCategoriesDeleted() {
  const { message } = App.useApp();
  const [categories, setCategories] = useState<CategoryDto[]>([]);
  const [loading, setLoading] = useState(true);

  async function fetchDeletedCategories() {
    setLoading(true);
    try {
      const data = await fetchAdminCopKategoriler();
      setCategories(data);
    } catch (error) {
      console.error(error);
      message.error('Silinen kategoriler yüklenirken hata oluştu.');
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    fetchDeletedCategories();
  }, []);

  async function handleForceDelete(id: string) {
    try {
      const res = await forceDeleteAdminKategori(id);
      message.success(res.message || 'Kategori kalıcı olarak silindi.');
      fetchDeletedCategories();
    } catch (error: any) {
      message.error(error.response?.data?.message || 'Silme işlemi başarısız.');
    }
  }

  async function handleUndoDelete(id: string) {
    try {
      const res = await undoDeleteAdminKategori(id);
      message.success(res.message || 'Kategori başarıyla geri yüklendi.');
      fetchDeletedCategories();
    } catch (error: any) {
      message.error(error.response?.data?.message || 'Geri yükleme işlemi başarısız.');
    }
  }

  const columns = [
    {
      title: 'Kategori Adı',
      dataIndex: 'name',
      key: 'name',
      render: (text: string) => <Tag color="default">{text}</Tag>,
    },
    {
      title: 'Silinme Tarihi',
      dataIndex: 'deleteDate',
      key: 'deleteDate',
      render: (date: string) => date ? <Tag color="red">{dayjs(date).format('DD/MM/YYYY HH:mm')}</Tag> : '-',
    },
    {
      title: 'Silen Kullanıcı',
      dataIndex: 'deletedBy',
      key: 'deletedBy',
    },
    {
      title: 'İşlemler',
      key: 'action',
      render: (_: any, record: CategoryDto) => (
        <Space size="middle">
          <Popconfirm
            title="Emin misiniz?"
            description="Kategori geri yüklenecektir."
            onConfirm={() => handleUndoDelete(record.id || '')}
            okText="Geri Yükle"
            cancelText="İptal"
          >
            <Tooltip title="Geri Yükle">
              <Button type="primary" icon={<UndoOutlined />} />
            </Tooltip>
          </Popconfirm>
          <Popconfirm
            title="Emin misiniz?"
            description="Bu kategori kalıcı olarak silinecektir! İşlem geri alınamaz."
            onConfirm={() => handleForceDelete(record.id || '')}
            okText="Kalıcı Sil"
            cancelText="İptal"
            okButtonProps={{ danger: true }}
          >
            <Tooltip title="Kalıcı Sil">
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
        <h1 className="text-2xl font-bold text-red-600">Silinen Kategoriler</h1>
        <Link href="/admin/categories">
          <Button type="primary" icon={<RollbackOutlined />}>
            Kategorilere Dön
          </Button>
        </Link>
      </div>

      <Table 
        columns={columns} 
        dataSource={categories} 
        rowKey="id" 
        loading={loading}
        pagination={{ defaultPageSize: 10, showSizeChanger: true }}
      />
    </div>
  );
}
