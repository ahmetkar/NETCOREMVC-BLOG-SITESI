import axiosInstance from '@/lib/axios';
import { MediaDto } from '@/types/dto';

export async function fetchAdminMediaList(): Promise<MediaDto[]> {
  const res = await axiosInstance.get('/admin/Media');
  return res.data;
}
export async function fetchAdminDeletedMedia(): Promise<MediaDto[]> {
  const res = await axiosInstance.get('/admin/Media/deleted');
  return res.data;
}


export async function uploadAdminMedia(formData: FormData): Promise<{ message?: string }> {
  const res = await axiosInstance.post('/admin/Media/upload', formData, {
    headers: { 'Content-Type': 'multipart/form-data' }
  });
  return res.data;
}

export async function deleteAdminMedia(id: string): Promise<{ message?: string }> {
  const res = await axiosInstance.delete(`/admin/Media/safe/${id}`);
  return res.data;
}

export async function forceDeleteAdminMedia(id: string): Promise<{ message?: string }> {
  const res = await axiosInstance.delete(`/admin/Media/force/${id}`);
  return res.data;
}

