'use client';

import { useEffect, useState } from 'react';
import { useSearchParams } from 'next/navigation';
import { dogrulaBultenAboneliktenCik, postBultenAboneliktenCik } from '@/services/visitor/generalService';

type PageState = 'checking' | 'ready' | 'invalid' | 'submitting' | 'completed';

export default function UnsubscribeClient() {
  const searchParams = useSearchParams();
  const id = searchParams.get('id') || '';
  const token = searchParams.get('token') || '';
  const [state, setState] = useState<PageState>('checking');
  const [errorMessage, setErrorMessage] = useState('');
  const hasRequiredParams = Boolean(id && token);
  const displayedState = hasRequiredParams ? state : 'invalid';

  useEffect(() => {
    if (!hasRequiredParams) return;

    let mounted = true;
    dogrulaBultenAboneliktenCik(id, token)
      .then(() => {
        if (mounted) setState('ready');
      })
      .catch((error: unknown) => {
        console.error(error);
        if (mounted) {
          setState('invalid');
          setErrorMessage('Bu abonelik bağlantısı geçersiz ya da daha önce kullanılmış.');
        }
      });

    return () => {
      mounted = false;
    };
  }, [hasRequiredParams, id, token]);

  const unsubscribe = async () => {
    setState('submitting');
    try {
      await postBultenAboneliktenCik(id, token);
      setState('completed');
    } catch (error: unknown) {
      console.error(error);
      setState('ready');
      setErrorMessage('İşlem tamamlanamadı. Lütfen daha sonra tekrar deneyin.');
    }
  };

  const title = displayedState === 'completed' ? 'Abonelik sonlandırıldı' : 'Bülten aboneliğinden çık';

  return (
    <section className="max-w-xl mx-auto px-4 py-20">
      <div className="rounded-2xl border border-gray-200 bg-white p-8 shadow-sm text-center">
        <h1 className="text-3xl font-bold text-gray-900">{title}</h1>
        {displayedState === 'checking' && <p className="mt-4 text-gray-600">Bağlantınız doğrulanıyor...</p>}
        {displayedState === 'ready' && (
          <>
            <p className="mt-4 text-gray-600">Onayladığınızda bu e-posta adresine artık yeni bülten gönderilmeyecek.</p>
            {errorMessage && <p className="mt-4 text-sm text-red-600">{errorMessage}</p>}
            <button onClick={unsubscribe} className="mt-8 rounded-lg bg-red-600 px-5 py-3 font-semibold text-white hover:bg-red-700">
              Abonelikten çık
            </button>
          </>
        )}
        {displayedState === 'submitting' && <p className="mt-4 text-gray-600">İşleminiz gerçekleştiriliyor...</p>}
        {displayedState === 'completed' && <p className="mt-4 text-gray-600">Artık bu e-posta adresine bülten gönderilmeyecek.</p>}
        {displayedState === 'invalid' && <p className="mt-4 text-red-600">{errorMessage || 'Abonelik bağlantısı eksik veya geçersiz.'}</p>}
      </div>
    </section>
  );
}
