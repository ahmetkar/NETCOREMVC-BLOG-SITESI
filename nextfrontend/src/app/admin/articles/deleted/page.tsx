'use client';
import { useEffect, useState } from 'react';
import { App,  Table, Button, Space, Popconfirm, Tag, Tooltip  } from 'antd';
import { DeleteOutlined, RollbackOutlined, UndoOutlined } from '@ant-design/icons';
import { fetchAdminCopMakaleler, undoDeleteAdminMakale, forceDeleteAdminMakale } from '@/services/admin/articleService';
import { ArticleDto } from '@/types/dto';
import Link from 'next/link';
import dayjs from 'dayjs';

export default function AdminArticlesDeleted() {
  const { message } = App.useApp();
  const [articles, setArticles] = useState<ArticleDto[]>([]);
  const [loading, setLoading] = useState(true);

  async function fetchDeletedArticles() {
    setLoading(true);
    try {
      const data = await fetchAdminCopMakaleler();
      setArticles(data);
    } catch (error) {
      console.error(error);
      message.error('Silinen makaleler yüklenirken hata oluştu.');
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    fetchDeletedArticles();
  }, []);

  async function handleForceDelete(id: string) {
    try {
      const res = await forceDeleteAdminMakale(id);
      message.success(res.message || 'Makale kalıcı olarak silindi.');
      fetchDeletedArticles();
    } catch (error: any) {
      message.error(error.response?.data?.message || 'Silme işlemi başarısız.');
    }
  }

  async function handleUndoDelete(id: string) {
    try {
      const res = await undoDeleteAdminMakale(id);
      message.success(res.message || 'Makale başarıyla geri yüklendi.');
      fetchDeletedArticles();
    } catch (error: any) {
      message.error(error.response?.data?.message || 'Geri yükleme işlemi başarısız.');
    }
  }

  const columns = [
    {
      title: 'Başlık',
      dataIndex: 'title',
      key: 'title',
      render: (text: string) => <strong>{text}</strong>,
    },
    {
      title: 'Kategori',
      dataIndex: ['category', 'name'],
      key: 'category',
      render: (categoryName: string) => <Tag color="default">{categoryName || 'Kategorisiz'}</Tag>,
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
      render: (_: any, record: ArticleDto) => (
        <Space size="middle">
          <Popconfirm
            title="Emin misiniz?"
            description="Makale geri yüklenecektir."
            onConfirm={() => handleUndoDelete(record.id)}
            okText="Geri Yükle"
            cancelText="İptal"
          >
            <Tooltip title="Geri Yükle">
              <Button type="primary" icon={<UndoOutlined />} />
            </Tooltip>
          </Popconfirm>
          <Popconfirm
            title="Emin misiniz?"
            description="Bu makale kalıcı olarak silinecektir! İşlem geri alınamaz."
            onConfirm={() => handleForceDelete(record.id)}
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
        <h1 className="text-2xl font-bold text-red-600">Silinen Makaleler</h1>
        <Link href="/admin/articles">
          <Button type="primary" icon={<RollbackOutlined />}>
            Makalelere Dön
          </Button>
        </Link>
      </div>

      <Table 
        columns={columns} 
        dataSource={articles} 
        rowKey="id" 
        loading={loading}
        pagination={{ defaultPageSize: 10, showSizeChanger: true }}
      />
    </div>
  );
}
