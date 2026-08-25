import { Suspense } from 'react';
import { Metadata } from 'next';
import UnsubscribeClient from '@/components/public/UnsubscribeClient';

export const dynamic = 'force-dynamic';

export const metadata: Metadata = {
  title: 'Abonelikten Çık | Blogger',
  robots: {
    index: false,
    follow: false,
  },
};

export default function UnsubscribePage() {
  return (
    <Suspense fallback={<div className="py-24 text-center">Sayfa hazırlanıyor...</div>}>
      <UnsubscribeClient />
    </Suspense>
  );
}
