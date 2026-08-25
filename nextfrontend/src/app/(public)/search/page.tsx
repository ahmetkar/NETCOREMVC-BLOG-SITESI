'use client';
import { useSearchParams, useRouter } from 'next/navigation';
import { useState, useEffect, Suspense } from 'react';
import { fetchArama } from '@/services/visitor/articleService';
import { ArticleDto } from '@/types/dto';
import { getImageUrl } from '@/lib/image-url';
import Link from 'next/link';

function SearchContent() {
  const searchParams = useSearchParams();
  const router = useRouter();
  const initialKeyword = searchParams?.get('keyword') || '';
  
  const [keyword, setKeyword] = useState(initialKeyword);
  const [results, setResults] = useState<ArticleDto[]>([]);
  const [loading, setLoading] = useState(!!initialKeyword);

  useEffect(() => {
    if (initialKeyword) {
      Promise.resolve().then(() => setLoading(true));
      fetchArama(initialKeyword)
        .then((res: any) => {
          const fetchedArticles = res.articles || res;
          setResults(Array.isArray(fetchedArticles) ? fetchedArticles : []);
        })
        .catch(err => console.error(err))
        .finally(() => setLoading(false));
    } else {
      setLoading(false);
      setResults([]);
    }
  }, [initialKeyword]);

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    if (keyword.trim()) {
      router.push(`/search?keyword=${encodeURIComponent(keyword)}`);
    }
  };

  return (
    <div className="w-full relative min-h-screen">
      
      {/* Decorative Aurora Elements */}
      <div className="absolute top-[10%] left-[20%] w-[40%] h-[40%] rounded-full bg-brand-primary/10 blur-[150px] pointer-events-none -z-10"></div>
      <div className="absolute top-[30%] right-[10%] w-[30%] h-[30%] rounded-full bg-brand-secondary/10 blur-[120px] pointer-events-none -z-10"></div>

      {/* Search Header */}
      <div className="max-w-[1000px] mx-auto px-4 sm:px-6 lg:px-12 py-20 lg:py-32 text-center">
         <span className="text-[11px] font-bold uppercase tracking-widest text-brand-primary mb-6 block">Arama</span>
         <h1 className="text-5xl sm:text-6xl lg:text-7xl font-display font-black text-brand-text mb-12 tracking-tight">
           Ne keşfetmek <br className="hidden sm:block"/> istiyorsunuz?
         </h1>
         
         <form onSubmit={handleSearch} className="max-w-3xl mx-auto flex neumorphic rounded-full p-2 pl-8 items-center bg-white/50 border border-brand-border/50 shadow-inner group transition-all focus-within:ring-4 focus-within:ring-brand-primary/10">
            <svg className="w-6 h-6 text-brand-muted shrink-0" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" /></svg>
            <input
              type="text"
              value={keyword}
              onChange={(e) => setKeyword(e.target.value)}
              placeholder="Makaleler, incelemeler, ipuçları..."
              className="w-full bg-transparent border-none outline-none text-xl font-medium text-brand-text placeholder:text-brand-muted/50 px-6 py-4"
            />
            <button type="submit" className="px-8 py-4 bg-brand-primary text-white text-[11px] font-bold uppercase tracking-widest rounded-full hover:bg-brand-primary/90 transition-all shadow-md shadow-brand-primary/20 shrink-0">
              Ara
            </button>
         </form>
      </div>

      {/* Results Section */}
      {initialKeyword && (
        <div className="max-w-[1200px] mx-auto px-4 sm:px-6 lg:px-12 pb-32">
           <div className="mb-10 text-center">
             <div className="inline-block px-6 py-2 rounded-full glass border border-white/40">
               <span className="text-sm font-bold text-brand-muted">
                 <strong className="text-brand-text">&quot;{initialKeyword}&quot;</strong> için <strong className="text-brand-primary">{results.length}</strong> sonuç bulundu
               </span>
             </div>
           </div>

           {loading ? (
             <div className="flex flex-col items-center justify-center py-20">
                <div className="w-12 h-12 border-4 border-brand-primary/30 border-t-brand-primary rounded-full animate-spin mb-4"></div>
                <div className="text-sm font-bold uppercase tracking-widest text-brand-primary">Aranıyor...</div>
             </div>
           ) : results.length === 0 ? (
             <div className="text-center py-20 glass rounded-3xl border border-white/40">
                <div className="w-20 h-20 bg-brand-primary/10 rounded-full flex items-center justify-center mx-auto mb-6">
                  <svg className="w-10 h-10 text-brand-primary" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M9.172 16.172a4 4 0 015.656 0M9 10h.01M15 10h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" /></svg>
                </div>
                <h3 className="text-3xl font-display font-bold text-brand-text mb-4">Sonuç Bulunamadı</h3>
                <p className="text-brand-muted text-lg font-medium">Lütfen farklı kelimelerle veya kategori sayfalarımızdan tekrar deneyin.</p>
             </div>
           ) : (
             <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
               {results.map((article: ArticleDto) => (
                 <div key={article.id} className="glass rounded-3xl overflow-hidden hover:shadow-2xl hover:shadow-brand-primary/10 transition-all duration-500 transform hover:-translate-y-2 border border-white/40 group">
                   <Link href={`/blog/${article.slug || article.id}`} className="block h-full flex flex-col">
                     <div className="w-full aspect-[4/3] bg-brand-border overflow-hidden relative">
                       <img 
                         src={getImageUrl(article.image?.fileName, 'https://via.placeholder.com/600x400')}
                         alt={article.title}
                         className="w-full h-full object-cover group-hover:scale-110 transition-transform duration-1000 ease-out"
                       />
                       <div className="absolute inset-0 bg-brand-primary/10 opacity-0 group-hover:opacity-100 transition-opacity duration-500"></div>
                       <div className="absolute top-4 left-4">
                         <span className="text-[10px] font-bold uppercase tracking-widest text-brand-text bg-white/90 backdrop-blur-md px-3 py-1 rounded-full shadow-sm">
                           {article.category?.name || 'GENEL'}
                         </span>
                       </div>
                     </div>
                     
                     <div className="p-6 md:p-8 flex flex-col flex-grow bg-white/50 backdrop-blur-sm">
                       <div className="flex items-center gap-2 mb-4 text-[11px] font-bold uppercase tracking-widest text-brand-muted">
                         <svg className="w-4 h-4 text-brand-secondary" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" /></svg>
                         <span>{new Date(article.createdDate).toLocaleDateString('tr-TR')}</span>
                       </div>
                       
                       <h4 className="text-2xl font-display font-bold text-brand-text leading-tight group-hover:text-brand-primary transition-colors mb-4 line-clamp-3">
                         {article.title}
                       </h4>
                       
                       <p className="text-brand-muted text-sm line-clamp-2 mt-auto font-medium">
                         {article.content ? article.content.replace(/<[^>]+>/g, '').substring(0, 150) : ''}...
                       </p>
                     </div>
                   </Link>
                 </div>
               ))}
             </div>
           )}
        </div>
      )}

    </div>
  );
}

export default function SearchPage() {
  return (
    <Suspense fallback={
      <div className="min-h-screen flex items-center justify-center">
         <div className="w-12 h-12 border-4 border-brand-primary/30 border-t-brand-primary rounded-full animate-spin"></div>
      </div>
    }>
      <SearchContent />
    </Suspense>
  );
}
