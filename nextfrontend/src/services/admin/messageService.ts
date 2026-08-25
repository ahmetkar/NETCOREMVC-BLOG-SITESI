import axiosInstance from '@/lib/axios';
import { MessageDto } from '@/types/dto';

export async function fetchAdminMesajlar(): Promise<MessageDto[]> {
  const res = await axiosInstance.get('/admin/Messages');
  return res.data;
}

export async function updateMesajOkunduDurumu(id: string, isRead: boolean): Promise<{ message?: string }> {
  const res = await axiosInstance.put(`/admin/Messages/${id}/read-status?isRead=${isRead}`);
  return res.data;
}

export async function deleteAdminMesaj(id: string): Promise<{ message?: string }> {
  const res = await axiosInstance.delete(`/admin/Messages/${id}`);
  return res.data;
}

