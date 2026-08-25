'use client';
import { useState, useEffect } from 'react';
import { fetchKategorilerLazy } from '@/services/visitor/categoryService';
import { getImageUrl } from '@/lib/image-url';
import Link from 'next/link';

export default function HomeClient({
  initialLayoutData,
  initialCategories
}: {
  initialLayoutData: any;
  initialCategories: any[];
}) {
  const [layoutData, setLayoutData] = useState<any>(initialLayoutData);
  const [categories, setCategories] = useState<any[]>(initialCategories);
  const [skip, setSkip] = useState(initialCategories.length);
  const [hasMore, setHasMore] = useState(initialCategories.length >= 2 && initialCategories.length < 5);
  const [loadingMore, setLoadingMore] = useState(false);

  const fetchCategories = async (currentSkip: number) => {
    if (loadingMore || !hasMore) return;
    setLoadingMore(true);
    try {
      const res = await fetchKategorilerLazy(currentSkip, 2);
      if (res.success) {
        const fetchedCategories = res.categories;
        if (fetchedCategories.length > 0) {
          setCategories(prev => {
            const existingIds = new Set(prev.map(c => c.id));
            const newCats = fetchedCategories.filter((c: any) => !existingIds.has(c.id));
            return [...prev, ...newCats];
          });
          const nextSkip = currentSkip + 2;
          setSkip(nextSkip);
          if (fetchedCategories.length < 2 || nextSkip >= 5) {
            setHasMore(false);
          }
        } else {
          setHasMore(false);
        }
      }
    } catch (err) {
      console.error(err);
    } finally {
      setLoadingMore(false);
    }
  };

  useEffect(() => {
    const handleScroll = () => {
      if (window.innerHeight + document.documentElement.scrollTop >= document.documentElement.offsetHeight - 500) {
        if (!loadingMore && hasMore) {
          fetchCategories(skip);
        }
      }
    };
    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, [skip, loadingMore, hasMore]);

  if (!layoutData) return null;

  const topArticles = layoutData.topArticles || [];
  const featured = layoutData.heroArticle || (topArticles.length > 0 ? topArticles[0] : null);
  const heroOthers = layoutData.featuredArticles || [];

  return (
    <div className="w-full relative pb-16">
      
      {/* 1. HERO SECTION */}
      <section className="pt-8 pb-16 md:pt-12 md:pb-20">
        <div className="max-w-[1400px] mx-auto px-4 sm:px-6 lg:px-12 flex flex-col lg:flex-row gap-8">
          {/* Main Featured - Glass Card */}
          {featured && (
            <div className="lg:w-2/3 glass rounded-3xl overflow-hidden relative group cursor-pointer shadow-xl shadow-black/5" onClick={() => window.location.href=`/blog/${featured.slug || featured.id}`}>
              <div className="w-full aspect-[16/9] lg:aspect-[4/3] bg-brand-border overflow-hidden relative">
                <img 
                  src={getImageUrl(featured.image?.fileName, 'https://via.placeholder.com/1200x800')}
                  alt={featured.title}
                  className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-1000 ease-out"
                />
                <div className="absolute inset-0 bg-gradient-to-t from-black/80 via-black/20 to-transparent"></div>
                
                <div className="absolute bottom-0 left-0 w-full p-8 md:p-12">
                  <div className="mb-4 flex flex-wrap items-center gap-3">
                    <span className="text-[11px] font-bold uppercase tracking-widest text-white bg-brand-primary/80 backdrop-blur-sm px-4 py-1.5 rounded-full">
                      {featured.category?.name || 'FEATURED'}
                    </span>
                    <span className="text-[11px] font-bold uppercase tracking-widest text-white/80 drop-shadow-md">
                       {new Date(featured.createdDate).toLocaleDateString('tr-TR')}
                    </span>
                  </div>
                  
                  <h1 className="text-3xl md:text-5xl lg:text-6xl font-display font-black text-white leading-tight mb-4 drop-shadow-lg group-hover:text-brand-secondary transition-colors duration-300">
                    {featured.title}
                  </h1>
                </div>
              </div>
            </div>
          )}

          {/* Hero Sidebar - 2 articles with images */}
          <div className="lg:w-1/3 flex flex-col gap-6">
            <div className="flex items-center gap-2 mb-2">
              <div className="w-2 h-2 rounded-full bg-brand-secondary animate-pulse"></div>
              <h3 className="text-sm font-bold uppercase tracking-widest text-brand-text">Öne Çıkanlar</h3>
            </div>
            
            {heroOthers.map((article: any) => (
              <div key={article.id} className="flex-1 glass rounded-3xl overflow-hidden relative group cursor-pointer shadow-md shadow-black/5" onClick={() => window.location.href=`/blog/${article.slug || article.id}`}>
                <div className="w-full h-full absolute inset-0 bg-brand-border z-0">
                  <img 
                    src={getImageUrl(article.image?.fileName, 'https://via.placeholder.com/600x400')}
                    alt={article.title}
                    className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-1000 ease-out"
                  />
                  <div className="absolute inset-0 bg-gradient-to-t from-black/90 via-black/40 to-black/10 group-hover:bg-black/40 transition-colors duration-500"></div>
                </div>

                <div className="relative z-10 p-6 flex flex-col h-full justify-end">
                   <div className="mb-3">
                     <span className="text-[10px] font-bold uppercase tracking-widest text-white bg-brand-primary/80 backdrop-blur-sm px-3 py-1 rounded-full inline-block">
                        {article.category?.name || 'ÖNE ÇIKAN'}
                     </span>
                   </div>
                   <h2 className="text-xl md:text-2xl font-display font-bold text-white leading-tight group-hover:text-brand-secondary transition-colors mb-3 line-clamp-3">
                     {article.title}
                   </h2>
                   <div className="flex items-center gap-2 text-[10px] font-bold uppercase tracking-widest text-white/80">
                      <div className="w-5 h-5 rounded-full bg-white/20 border border-white/40 flex items-center justify-center overflow-hidden relative">
                        {article.user?.image ? (
                          <img 
                            src={getImageUrl(article.user.image.fileName)}
                            alt={`${article.user.firstName} ${article.user.lastName}`}
                            className="w-full h-full object-cover"
                          />
                        ) : (
                          <>{article.user?.firstName?.charAt(0)}{article.user?.lastName?.charAt(0)}</>
                        )}
                      </div>
                      <span>{article.user?.firstName} {article.user?.lastName}</span>
                   </div>
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* DYNAMIC CATEGORY SECTIONS (Lazy Loaded) */}
      {categories.map((category: any, catIndex: number) => {
        if (!category.articles || category.articles.length === 0) return null;
        
        const isReversed = category.articles.length > 1 ? (catIndex % 2 !== 0) : false;

        return (
          <section key={category.id} className="py-12 relative border-t border-brand-border/20">
            <div className="max-w-[1400px] mx-auto px-4 sm:px-6 lg:px-12">
              <div className="flex items-end justify-between mb-10">
                 <div>
                   <h2 className="text-4xl md:text-5xl font-display font-black text-brand-text text-gradient mb-2">{category.name}</h2>
                   <p className="text-brand-muted font-medium">Bu kategoriden en güncel içerikler.</p>
                 </div>
                 <Link href={`/kategoriler/${category.slug || category.id}`} className="hidden md:flex items-center gap-2 text-[11px] font-bold uppercase tracking-widest text-brand-text hover:text-brand-primary transition-colors">
                   Tümünü Gör <span className="text-brand-primary">&rarr;</span>
                 </Link>
              </div>
              
              <div className={`flex flex-col gap-8 ${isReversed ? 'lg:flex-row-reverse' : 'lg:flex-row'}`}>
                {/* Large Card - Neumorphic */}
                {category.articles[0] && (
                  <div className="lg:w-1/2 neumorphic p-6 md:p-8 flex flex-col">
                    <Link href={`/blog/${category.articles[0].slug || category.articles[0].id}`} className="group block h-full flex flex-col">
                       <div className="w-full aspect-[16/10] bg-brand-border overflow-hidden mb-8 rounded-2xl relative shadow-inner">
                         <img src={getImageUrl(category.articles[0].image?.fileName, 'https://via.placeholder.com/800x600')} alt="" className="w-full h-full object-cover group-hover:scale-105 transition-transform duration-700 ease-out" />
                         <div className="absolute inset-0 bg-brand-primary/10 group-hover:bg-transparent transition-colors duration-500"></div>
                       </div>
                       <h3 className="text-3xl md:text-4xl font-display font-black text-brand-text leading-tight mb-4 group-hover:text-brand-primary transition-colors">
                         {category.articles[0].title}
                       </h3>
                       <p className="text-brand-muted text-base line-clamp-3 mb-6 font-medium flex-grow">
                          {category.articles[0].content ? category.articles[0].content.replace(/<[^>]+>/g, '') : ''}
                       </p>
                       <div className="mt-auto pt-4 border-t border-brand-border/50 text-[11px] font-bold uppercase tracking-widest text-brand-muted flex items-center justify-between">
                          <span>{new Date(category.articles[0].createdDate).toLocaleDateString('tr-TR')}</span>
                          <span className="text-brand-primary group-hover:text-brand-secondary transition-colors">Devamını Oku &rarr;</span>
                       </div>
                    </Link>
                  </div>
                )}
                
                {/* Stacked Cards - Glass */}
                <div className="lg:w-1/2 flex flex-col gap-8">
                  {category.articles.slice(1, 3).map((article: any) => (
                    <div key={article.id} className="flex-1 flex flex-col glass rounded-2xl p-4 hover:shadow-lg transition-shadow">
                      <Link href={`/blog/${article.slug || article.id}`} className="group flex flex-col sm:flex-row gap-4 h-full">
                         <div className="w-full sm:w-1/3 aspect-[16/9] sm:aspect-square md:aspect-[4/3] bg-brand-border overflow-hidden rounded-xl relative shrink-0">
                            <img src={getImageUrl(article.image?.fileName, 'https://via.placeholder.com/400x400')} alt="" className="absolute inset-0 w-full h-full object-cover group-hover:scale-105 transition-transform duration-700 ease-out" />
                         </div>
                         <div className="w-full sm:w-2/3 flex flex-col flex-grow py-1">
                            <span className="text-[10px] font-bold uppercase tracking-widest text-brand-primary mb-2">
                               {article.category?.name || category.name}
                            </span>
                            <h4 className="text-lg md:text-xl font-display font-bold text-brand-text leading-tight group-hover:text-brand-secondary transition-colors mb-2 line-clamp-2">
                              {article.title}
                            </h4>
                            <p className="text-brand-muted text-sm line-clamp-2 mb-3 flex-grow">
                               {article.content ? article.content.replace(/<[^>]+>/g, '') : ''}
                            </p>
                            <span className="text-[11px] font-bold uppercase tracking-widest text-brand-muted mt-auto flex items-center gap-1 group-hover:text-brand-text transition-colors">
                              <svg className="w-4 h-4 text-brand-secondary" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" /></svg>
                              {new Date(article.createdDate).toLocaleDateString('tr-TR')}
                            </span>
                         </div>
                      </Link>
                    </div>
                  ))}
                </div>
              </div>
            </div>
          </section>
        );
      })}

      {loadingMore && (
        <div className="py-12 flex items-center justify-center">
          <div className="flex flex-col items-center">
            <div className="w-8 h-8 border-2 border-brand-primary/30 border-t-brand-primary rounded-full animate-spin mb-3"></div>
            <div className="text-[10px] font-bold uppercase tracking-widest text-brand-primary">Daha Fazla Kategori Yükleniyor...</div>
          </div>
        </div>
      )}

    </div>
  );
}
