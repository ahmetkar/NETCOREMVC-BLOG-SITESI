import axiosInstance from '@/lib/axios';
import { LayoutDataDto, ContactRequestDto } from '@/types/dto';

export async function fetchGenelVeriler(): Promise<LayoutDataDto> {
  const res = await axiosInstance.get('/Public/layout-data');
  return res.data;
}

export async function postIletisimMesaji(data: ContactRequestDto): Promise<{ success: boolean; message?: string }> {
  const res = await axiosInstance.post('/Public/iletisim', data);
  return res.data;
}

export async function postBultenAbone(email: string): Promise<{ success: boolean; message?: string }> {
  const res = await axiosInstance.post('/Public/newsletter/subscribe', { email });
  return res.data;
}

export async function dogrulaBultenAboneliktenCik(id: string, token: string): Promise<{ success: boolean; message?: string }> {
  const res = await axiosInstance.get('/Public/newsletter/unsubscribe/validate', { params: { id, token } });
  return res.data;
}

export async function postBultenAboneliktenCik(id: string, token: string): Promise<{ success: boolean; message?: string }> {
  const res = await axiosInstance.post('/Public/newsletter/unsubscribe', { id, token });
  return res.data;
}

export async function postMakaleYorum(formData: FormData): Promise<{ success: boolean; message?: string }> {
  const res = await axiosInstance.post('/Public/article/comment', formData, {
    headers: { 'Content-Type': 'multipart/form-data' }
  });
  return res.data;
}

