const apiUrl = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5190/api';
const apiOrigin = apiUrl.replace(/\/api\/?$/, '').replace(/\/$/, '');

export function getImageUrl(fileName?: string | null, fallback?: string): string | undefined {
  if (!fileName) return fallback;
  if (/^https?:\/\//i.test(fileName) || fileName.startsWith('//')) return fileName;

  return `${apiOrigin}/${fileName.replace(/^\/+/, '')}`;
}
