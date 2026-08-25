import { Metadata } from 'next';
import AboutClient from '@/components/public/AboutClient';

export const dynamic = 'force-dynamic';

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5190/api';
const SITE_URL = process.env.NEXT_PUBLIC_SITE_URL || 'http://localhost:3000';

async function getSettings() {
  try {
    const res = await fetch(`${API_URL}/Public/layout-data`, { cache: 'no-store' });
    if (!res.ok) throw new Error('API layout-data fetch failed');
    const data = await res.json();
    return data?.settings || null;
  } catch (error) {
    console.error('Error fetching settings on server:', error);
    return null;
  }
}

export async function generateMetadata(): Promise<Metadata> {
  const settings = await getSettings();
  const siteTitle = settings?.siteTitle || 'Blogger';
  const title = `Hakkımızda | ${siteTitle}`;
  const description = settings?.aboutUsDescription || 'Blogger hakkında detaylı bilgi, vizyonumuz ve değerlerimiz.';

  return {
    title,
    description,
    metadataBase: new URL(SITE_URL),
    alternates: {
      canonical: '/hakkimizda',
    },
    openGraph: {
      type: 'website',
      locale: 'tr_TR',
      url: `${SITE_URL}/hakkimizda`,
      title,
      description,
      siteName: siteTitle,
    },
    twitter: {
      card: 'summary_large_image',
      title,
      description,
    },
  };
}

export default async function Page() {
  const settings = await getSettings();

  // Structured Data (JSON-LD Breadcrumb)
  const jsonLd = {
    '@context': 'https://schema.org',
    '@type': 'BreadcrumbList',
    'itemListElement': [
      {
        '@type': 'ListItem',
        'position': 1,
        'name': 'Ana Sayfa',
        'item': SITE_URL
      },
      {
        '@type': 'ListItem',
        'position': 2,
        'name': 'Hakkımızda',
        'item': `${SITE_URL}/hakkimizda`
      }
    ]
  };

  return (
    <>
      <script
        type="application/ld+json"
        dangerouslySetInnerHTML={{ __html: JSON.stringify(jsonLd) }}
      />
      <AboutClient initialSettings={settings} />
    </>
  );
}
