import axiosInstance from '@/lib/axios';
import { UserDto } from '@/types/dto';

export async function fetchAdminKullanicilar(): Promise<UserDto[]> {
  const res = await axiosInstance.get('/admin/User');
  return res.data;
}

export async function fetchAdminRoller(): Promise<{ id: string; name: string }[]> {
  const res = await axiosInstance.get('/admin/User/roles');
  return res.data;
}

export async function createAdminKullanici(data: any): Promise<{ message?: string }> {
  const res = await axiosInstance.post('/admin/User', data);
  return res.data;
}

export async function updateAdminKullanici(data: any): Promise<{ message?: string }> {
  const res = await axiosInstance.put('/admin/User', data);
  return res.data;
}

export async function deleteAdminKullanici(id: string): Promise<{ message?: string }> {
  const res = await axiosInstance.delete(`/admin/User/${id}`);
  return res.data;
}

