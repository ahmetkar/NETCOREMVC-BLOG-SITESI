'use client';

import { useEffect, useState } from 'react';
import { App, Button, Input, Popconfirm, Switch, Table, Tag, Tooltip } from 'antd';
import { DeleteOutlined, SearchOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';
import { fetchAdminAboneler, deleteAdminAbone, updateAdminSubscriberStatus } from '@/services/admin/settingSubscriberService';
import { SubscriberDto } from '@/types/dto';

export default function AdminSubscribers() {
  const { message } = App.useApp();
  const [subscribers, setSubscribers] = useState<SubscriberDto[]>([]);
  const [loading, setLoading] = useState(true);
  const [search, setSearch] = useState('');

  useEffect(() => {
    let active = true;

    fetchAdminAboneler()
      .then((data) => {
        if (active) setSubscribers(data);
      })
      .catch((error: unknown) => {
        console.error(error);
        if (active) message.error('Aboneler yüklenirken hata oluştu.');
      })
      .finally(() => {
        if (active) setLoading(false);
      });

    return () => {
      active = false;
    };
  }, [message]);

  const handleDelete = async (id: string) => {
    try {
      const response = await deleteAdminAbone(id);
      message.success(response?.message || 'Abone listeden kaldırıldı.');
      setSubscribers((current) => current.filter((subscriber) => subscriber.id !== id));
    } catch (error: unknown) {
      console.error(error);
      message.error('Abone silinirken hata oluştu.');
    }
  };

  const handleStatusChange = async (subscriber: SubscriberDto, isActive: boolean) => {
    try {
      const response = await updateAdminSubscriberStatus(subscriber.id, isActive);
      message.success(response?.message || 'Abone durumu güncellendi.');
      setSubscribers((current) => current.map((item) => item.id === subscriber.id ? { ...item, isActive } : item));
    } catch (error: unknown) {
      console.error(error);
      message.error('Abone durumu güncellenemedi.');
    }
  };

  const filteredSubscribers = subscribers.filter((subscriber) =>
    subscriber.email.toLocaleLowerCase('tr-TR').includes(search.toLocaleLowerCase('tr-TR')),
  );
  const activeSubscriberCount = subscribers.filter((subscriber) => subscriber.isActive).length;

  const columns = [
    {
      title: 'Bülten Durumu',
      dataIndex: 'isActive',
      key: 'isActive',
      render: (isActive: boolean, subscriber: SubscriberDto) => (
        <div className="flex items-center gap-2">
          <Switch checked={isActive} checkedChildren="Açık" unCheckedChildren="Kapalı" onChange={(value) => handleStatusChange(subscriber, value)} />
          <Tag color={isActive ? 'green' : 'default'}>{isActive ? 'Gönderilir' : 'Gönderilmez'}</Tag>
        </div>
      ),
    },
    {
      title: 'E-posta',
      dataIndex: 'email',
      key: 'email',
      render: (email: string) => <a href={`mailto:${email}`}>{email}</a>,
    },
    {
      title: 'Abonelik Tarihi',
      dataIndex: 'createdDate',
      key: 'createdDate',
      render: (date?: string) => (date ? dayjs(date).format('DD.MM.YYYY HH:mm') : '-'),
      sorter: (a: SubscriberDto, b: SubscriberDto) => dayjs(a.createdDate).valueOf() - dayjs(b.createdDate).valueOf(),
      defaultSortOrder: 'descend' as const,
    },
    {
      title: 'İşlemler',
      key: 'actions',
      width: 120,
      render: (_: unknown, subscriber: SubscriberDto) => (
        <Popconfirm
          title="Aboneyi kaldır"
          description="Bu e-posta yeni bülten gönderimlerinden çıkarılır."
          okText="Kaldır"
          cancelText="İptal"
          okButtonProps={{ danger: true }}
          onConfirm={() => handleDelete(subscriber.id)}
        >
          <Tooltip title="Abonelikten çıkar">
            <Button danger icon={<DeleteOutlined />} aria-label="Aboneyi kaldır" />
          </Tooltip>
        </Popconfirm>
      ),
    },
  ];

  return (
    <div>
      <div className="flex flex-col gap-4 mb-6 sm:flex-row sm:items-center sm:justify-between">
        <div>
          <h1 className="text-2xl font-bold">Bülten Aboneleri</h1>
          <p className="text-gray-500 mt-1">Gönderime açık: {activeSubscriberCount} / Toplam: {subscribers.length}</p>
        </div>
        <Input
          allowClear
          prefix={<SearchOutlined />}
          placeholder="E-posta ara"
          value={search}
          onChange={(event) => setSearch(event.target.value)}
          className="sm:w-72"
        />
      </div>

      <Table
        columns={columns}
        dataSource={filteredSubscribers}
        rowKey="id"
        loading={loading}
        pagination={{ defaultPageSize: 10, showSizeChanger: true, showTotal: (total) => `Toplam ${total} abone` }}
        locale={{ emptyText: search ? 'Aramanızla eşleşen abone bulunamadı.' : 'Henüz aktif abone yok.' }}
      />
    </div>
  );
}
