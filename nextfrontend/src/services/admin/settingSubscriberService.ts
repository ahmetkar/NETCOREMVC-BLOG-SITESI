import axiosInstance from '@/lib/axios';
import { UserDto, SettingsDto, SubscriberDto } from '@/types/dto';

export async function fetchAdminProfil(): Promise<UserDto> {
  const res = await axiosInstance.get('/admin/User/profile');
  return res.data;
}

export async function updateAdminProfil(formData: FormData): Promise<{ message?: string }> {
  const res = await axiosInstance.put('/admin/User/profile', formData, {
    headers: { 'Content-Type': 'multipart/form-data' }
  });
  return res.data;
}

export async function fetchAdminAyarlar(): Promise<SettingsDto> {
  const res = await axiosInstance.get('/admin/Settings');
  return res.data;
}

export async function updateAdminAyarlar(formData: FormData): Promise<{ message?: string }> {
  const res = await axiosInstance.put('/admin/Settings', formData, {
    headers: { 'Content-Type': 'multipart/form-data' }
  });
  return res.data;
}

export async function fetchAdminAboneler(): Promise<SubscriberDto[]> {
  const res = await axiosInstance.get('/admin/Subscribers');
  return res.data;
}

export async function deleteAdminAbone(id: string): Promise<{ message?: string }> {
  const res = await axiosInstance.delete(`/admin/Subscribers/${id}`);
  return res.data;
}
export async function updateAdminSubscriberStatus(id: string, isActive: boolean): Promise<{ message?: string }> {
  const res = await axiosInstance.put(`/admin/Subscribers/${id}/status`, { isActive });
  return res.data;
}


