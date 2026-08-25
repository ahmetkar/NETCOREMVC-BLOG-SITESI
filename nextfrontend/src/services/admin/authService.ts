import axiosInstance from '@/lib/axios';
import { LoginRequestDto, LoginResponseDto } from '@/types/dto';

export async function yoneticiGiris(data: LoginRequestDto): Promise<LoginResponseDto> {
  const res = await axiosInstance.post('/Auth/login', data);
  return res.data;
}

