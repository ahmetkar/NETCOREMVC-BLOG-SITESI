'use client';
import { useEffect, useState } from 'react';
import { App,  Table, Button, Space, Popconfirm, Tag, Tooltip  } from 'antd';
import { EditOutlined, DeleteOutlined, PlusOutlined, RollbackOutlined, MessageOutlined } from '@ant-design/icons';
import { fetchAdminMakaleler, deleteAdminMakale } from '@/services/admin/articleService';
import { ArticleDto } from '@/types/dto';
import dayjs from 'dayjs';
import Link from 'next/link';

export default function AdminArticles() {
  const { message } = App.useApp();
  const [articles, setArticles] = useState<ArticleDto[]>([]);
  const [loading, setLoading] = useState(true);

  async function fetchArticles() {
    setLoading(true);
    try {
      const data = await fetchAdminMakaleler();
      setArticles(data);
    } catch (error) {
      console.error(error);
      message.error('Makaleler yüklenirken hata oluştu.');
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    fetchArticles();
  }, []);

  async function handleDelete(id: string) {
    try {
      const res = await deleteAdminMakale(id);
      message.success(res.message || 'Makale çöp kutusuna taşındı.');
      fetchArticles();
    } catch (error) {
      message.error('Makale silinirken hata oluştu.');
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
      render: (categoryName: string) => <Tag color="blue">{categoryName || 'Kategorisiz'}</Tag>,
    },
    {
      title: 'Görüntülenme',
      dataIndex: 'viewCount',
      key: 'viewCount',
    },
    {
      title: 'Oluşturulma Tarihi',
      dataIndex: 'createdDate',
      key: 'createdDate',
      render: (date: string) => dayjs(date).format('DD/MM/YYYY HH:mm'),
    },
    {
      title: 'İşlemler',
      key: 'action',
      render: (_: any, record: ArticleDto) => (
        <Space size="middle">
          <Link href={`/admin/articles/update/${record.id}`}>
            <Tooltip title="Düzenle">
              <Button type="primary" icon={<EditOutlined />} />
            </Tooltip>
          </Link>
          <Popconfirm
            title="Emin misiniz?"
            description="Makale çöp kutusuna taşınacaktır."
            onConfirm={() => handleDelete(record.id)}
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
        <h1 className="text-2xl font-bold">Makale Yönetimi</h1>
        <Space>
          <Link href="/admin/articles/comments">
            <Button type="default" icon={<MessageOutlined />}>
              Yorumlar
            </Button>
          </Link>
          <Link href="/admin/articles/deleted">
            <Button type="default" icon={<RollbackOutlined />}>
              Çöp Kutusu
            </Button>
          </Link>
          <Link href="/admin/articles/add">
            <Button type="primary" icon={<PlusOutlined />}>
              Yeni Makale Ekle
            </Button>
          </Link>
        </Space>
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

