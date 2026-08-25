import { Metadata } from 'next';
import HomeClient from '@/components/public/HomeClient';

export const dynamic = 'force-dynamic';

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5190/api';
const SITE_URL = process.env.NEXT_PUBLIC_SITE_URL || 'http://localhost:3000';

async function getHomeData() {
  try {
    const layoutRes = await fetch(`${API_URL}/Public/layout-data`, { cache: 'no-store' });
    if (!layoutRes.ok) throw new Error('API layout-data fetch failed');
    const layoutData = await layoutRes.json();

    const categoriesRes = await fetch(`${API_URL}/Public/categories-lazy?skip=0&take=2`, { cache: 'no-store' });
    let initialCategories = [];
    if (categoriesRes.ok) {
      const catData = await categoriesRes.json();
      initialCategories = catData.categories || [];
    }

    return { layoutData, initialCategories };
  } catch (error) {
    console.error('Error fetching home data on server:', error);
    return { layoutData: null, initialCategories: [] };
  }
}

export async function generateMetadata(): Promise<Metadata> {
  const { layoutData } = await getHomeData();
  const settings = layoutData?.settings;
  const title = settings?.siteTitle || 'TechBlog - Premium Blog Platformu';
  const description = settings?.footerDescription || 'Dijital dünyanın nabzını tutan, derinlemesine analizler sunan modern yayın platformu.';

  return {
    title,
    description,
    metadataBase: new URL(SITE_URL),
    alternates: {
      canonical: '/',
    },
    openGraph: {
      type: 'website',
      locale: 'tr_TR',
      url: SITE_URL,
      title,
      description,
      siteName: title,
    },
    twitter: {
      card: 'summary_large_image',
      title,
      description,
    },
  };
}

export default async function Page() {
  const { layoutData, initialCategories } = await getHomeData();

  const settings = layoutData?.settings;
  const title = settings?.siteTitle || 'Blogger';
  const description = settings?.footerDescription || 'Premium Blog Platformu';

  // Structured Data (JSON-LD)
  const jsonLd = {
    '@context': 'https://schema.org',
    '@type': 'WebSite',
    'name': title,
    'url': SITE_URL,
    'description': description,
    'potentialAction': {
      '@type': 'SearchAction',
      'target': `${SITE_URL}/search?keyword={search_term_string}`,
      'query-input': 'required name=search_term_string'
    }
  };

  return (
    <>
      <script
        type="application/ld+json"
        dangerouslySetInnerHTML={{ __html: JSON.stringify(jsonLd) }}
      />
      <HomeClient initialLayoutData={layoutData} initialCategories={initialCategories} />
    </>
  );
}
