'use client';
import { useEffect, useState } from 'react';
import { App,  Table, Button, Space, Popconfirm, Tag, Tooltip  } from 'antd';
import { DeleteOutlined, CheckOutlined, RollbackOutlined, CloseOutlined } from '@ant-design/icons';
import { fetchAdminYorumlar, deleteAdminYorum, updateYorumOnay } from '@/services/admin/commentService';
import { CommentDto } from '@/types/dto';
import Link from 'next/link';
import dayjs from 'dayjs';

export default function AdminComments() {
  const { message } = App.useApp();
  const [comments, setComments] = useState<CommentDto[]>([]);
  const [loading, setLoading] = useState(true);

  async function fetchComments() {
    setLoading(true);
    try {
      const data = await fetchAdminYorumlar();
      setComments(data);
    } catch (error) {
      console.error(error);
      message.error('Yorumlar yüklenirken hata oluştu.');
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    fetchComments();
  }, []);

  async function handleDelete(articleId: string, commentId: string) {
    try {
      const res = await deleteAdminYorum(articleId, commentId);
      message.success(res.message || 'Yorum başarıyla silindi.');
      fetchComments();
    } catch (error: any) {
      message.error(error.response?.data?.message || 'Yorum silinirken hata oluştu.');
    }
  }

  async function handleApprove(articleId: string, commentId: string, approved: boolean) {
    try {
      const res = await updateYorumOnay(articleId, commentId, approved);
      message.success(res.message || 'Yorum durumu güncellendi.');
      fetchComments();
    } catch (error: any) {
      message.error(error.response?.data?.message || 'Yorum durumu güncellenirken hata oluştu.');
    }
  }

  const columns = [
    {
      title: 'Gönderen',
      dataIndex: 'name',
      key: 'name',
      render: (text: string) => <strong>{text}</strong>,
    },
    {
      title: 'Yorum',
      dataIndex: 'commentText',
      key: 'commentText',
      width: '40%',
    },
    {
      title: 'Makale',
      dataIndex: ['article', 'title'],
      key: 'article',
      render: (text: string) => <Tag color="blue">{text}</Tag>,
    },
    {
      title: 'Tarih',
      dataIndex: 'createdDate',
      key: 'createdDate',
      render: (date: string) => dayjs(date).format('DD/MM/YYYY HH:mm'),
    },
    {
      title: 'Durum',
      dataIndex: 'isAprroved',
      key: 'isAprroved',
      render: (isAprroved: boolean) => (
        <Tag color={isAprroved ? 'success' : 'warning'}>
          {isAprroved ? 'Onaylı' : 'Onay Bekliyor'}
        </Tag>
      ),
    },
    {
      title: 'İşlemler',
      key: 'action',
      render: (_: any, record: CommentDto) => (
        <Space size="middle">
          {!record.isAprroved ? (
            <Tooltip title="Onayla">
              <Button 
                type="primary" 
                style={{ backgroundColor: '#52c41a', borderColor: '#52c41a' }} 
                icon={<CheckOutlined />} 
                onClick={() => handleApprove(record.articleId || '', record.id || '', true)}
              />
            </Tooltip>
          ) : (
            <Tooltip title="Onayı Kaldır">
              <Button 
                type="default" 
                icon={<CloseOutlined />} 
                onClick={() => handleApprove(record.articleId || '', record.id || '', false)}
              />
            </Tooltip>
          )}
          
          <Popconfirm
            title="Emin misiniz?"
            description="Bu yorum kalıcı olarak silinecektir."
            onConfirm={() => handleDelete(record.articleId || '', record.id || '')}
            okText="Sil"
            cancelText="İptal"
            okButtonProps={{ danger: true }}
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
        <h1 className="text-2xl font-bold">Yorum Yönetimi</h1>
        <Link href="/admin/articles">
          <Button type="primary" icon={<RollbackOutlined />}>
            Makalelere Dön
          </Button>
        </Link>
      </div>

      <Table 
        columns={columns} 
        dataSource={comments} 
        rowKey="id" 
        loading={loading}
        pagination={{ defaultPageSize: 10, showSizeChanger: true }}
      />
    </div>
  );
}
