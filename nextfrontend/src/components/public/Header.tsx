'use client';
import { useState, useEffect } from 'react';
import Link from 'next/link';
import { getImageUrl } from '@/lib/image-url';
import { useRouter } from 'next/navigation';
import { useLayoutData } from '@/providers/LayoutDataProvider';

export default function Header() {
  const layoutData = useLayoutData();
  const settings = layoutData?.settings || {};
  const categories = layoutData?.navCategories || [];

  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);
  const [isSearchOpen, setIsSearchOpen] = useState(false);
  const [keyword, setKeyword] = useState('');
  const [scrolled, setScrolled] = useState(false);
  const router = useRouter();

  useEffect(() => {
    const handleScroll = () => {
      setScrolled(window.scrollY > 20);
    };
    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

  const handleSearch = (e: React.FormEvent) => {
    e.preventDefault();
    if (keyword.trim()) {
      router.push(`/search?keyword=${encodeURIComponent(keyword)}`);
      setIsSearchOpen(false);
    }
  };

  return (
    <>
      <header className={`fixed top-0 w-full z-50 transition-all duration-300 ${scrolled ? 'py-3' : 'py-5'}`}>
        <div className="max-w-[1600px] mx-auto px-4 sm:px-6 lg:px-12">
          <div className={`glass rounded-2xl px-6 py-4 flex justify-between items-center transition-all duration-300 ${scrolled ? 'shadow-lg border-brand-primary/20' : ''}`}>

            {/* Logo */}
            <div className="flex-shrink-0 flex items-center">
              <Link href="/" className="group flex items-center">
                {settings?.logoImage?.fileName ? (
                  <img src={getImageUrl(settings.logoImage.fileName)} alt={settings.siteTitle || "Logo"} className="h-8 w-auto object-contain transition-transform group-hover:scale-105 duration-300" />
                ) : (
                  <span className="text-2xl font-display font-black tracking-tight text-black">
                    {settings.siteTitle || 'Blogger.'}
                  </span>
                )}
              </Link>
            </div>

            {/* Desktop Navigation */}
            <nav className="hidden lg:flex items-center space-x-8">
              <Link href="/" className="text-sm font-semibold tracking-wide text-brand-text hover:text-brand-primary transition-colors">Ana Sayfa</Link>

              <div className="relative group">
                <Link href="/kategoriler" className="text-sm font-semibold tracking-wide text-brand-text hover:text-brand-primary transition-colors flex items-center">
                  Kategoriler
                  <svg className="w-4 h-4 ml-1 opacity-50 group-hover:opacity-100 transition-opacity" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M19 9l-7 7-7-7"></path></svg>
                </Link>
                {/* Dropdown menu */}
                <div className="absolute top-full left-1/2 -translate-x-1/2 pt-4 opacity-0 invisible group-hover:opacity-100 group-hover:visible transition-all duration-200">
                  <div className="glass rounded-xl shadow-xl border border-white/40 p-3 min-w-[200px] flex flex-col gap-1">
                    {categories.slice(0, 5).map((cat: any) => (
                      <Link key={cat.id} href={`/kategoriler/${cat.slug || cat.id}`} className="px-4 py-2 rounded-lg hover:bg-brand-primary/10 text-brand-text hover:text-brand-primary text-sm font-medium transition-colors">
                        {cat.name}
                      </Link>
                    ))}
                    <Link href="/kategoriler" className="px-4 py-2 mt-2 border-t border-brand-border/50 text-brand-secondary text-xs font-bold uppercase tracking-wider text-center hover:bg-brand-secondary/10 rounded-lg transition-colors">
                      Tümünü Gör
                    </Link>
                  </div>
                </div>
              </div>

              <Link href="/hakkimizda" className="text-sm font-semibold tracking-wide text-brand-text hover:text-brand-primary transition-colors">Hakkımızda</Link>
              <Link href="/iletisim" className="text-sm font-semibold tracking-wide text-brand-text hover:text-brand-primary transition-colors">İletişim</Link>
            </nav>

            <div className="flex items-center space-x-4">
              {/* Search Toggle */}
              <button
                onClick={() => setIsSearchOpen(!isSearchOpen)}
                className="p-2 text-brand-text hover:text-brand-primary hover:bg-brand-primary/10 rounded-full transition-all"
                aria-label="Ara"
              >
                {isSearchOpen ? (
                  <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M6 18L18 6M6 6l12 12"></path></svg>
                ) : (
                  <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"></path></svg>
                )}
              </button>


              {/* Mobile Menu Toggle */}
              <button
                className="lg:hidden p-2 text-brand-text hover:bg-black/5 rounded-full transition-colors focus:outline-none"
                onClick={() => setIsMobileMenuOpen(!isMobileMenuOpen)}
                aria-label={isMobileMenuOpen ? "Menüyü Kapat" : "Menüyü Aç"}
              >
                <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="1.5" d={isMobileMenuOpen ? "M6 18L18 6M6 6l12 12" : "M4 6h16M4 12h16M4 18h16"}></path>
                </svg>
              </button>
            </div>
          </div>
        </div>
      </header>

      {/* Search Bar Fullscreen Overlay */}
      <div className={`fixed inset-0 z-40 bg-brand-bg/95 backdrop-blur-xl transition-all duration-300 flex items-center justify-center ${isSearchOpen ? 'opacity-100 visible' : 'opacity-0 invisible'}`}>
        <div className="w-full max-w-3xl px-6">
          <h3 className="text-brand-primary font-bold uppercase tracking-widest text-sm mb-4">Ne aramak istersiniz?</h3>
          <form onSubmit={handleSearch} className="relative flex items-center border-b-2 border-brand-text focus-within:border-brand-primary transition-colors pb-2">
            <input
              type="text"
              placeholder="Arama yapın..."
              value={keyword}
              onChange={(e) => setKeyword(e.target.value)}
              className="w-full text-4xl md:text-5xl font-display font-black text-brand-text bg-transparent border-none outline-none placeholder:text-brand-muted/30"
              autoFocus={isSearchOpen}
            />
            <button type="submit" aria-label="Aramayı Başlat" className="text-brand-text hover:text-brand-primary absolute right-0">
              <svg className="w-10 h-10" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M14 5l7 7m0 0l-7 7m7-7H3"></path></svg>
            </button>
          </form>
          <button onClick={() => setIsSearchOpen(false)} aria-label="Arama ekranını kapat" className="mt-8 text-brand-muted hover:text-brand-text transition-colors flex items-center text-sm font-medium">
            <svg className="w-4 h-4 mr-2" fill="none" stroke="currentColor" viewBox="0 0 24 24"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M10 19l-7-7m0 0l7-7m-7 7h18"></path></svg>
            Geri Dön
          </button>
        </div>
      </div>

      {/* Mobile Menu Overlay */}
      <div className={`lg:hidden fixed inset-0 z-40 bg-brand-bg/95 backdrop-blur-lg transition-all duration-300 ${isMobileMenuOpen ? 'opacity-100 visible' : 'opacity-0 invisible'}`}>
        <nav className="flex flex-col items-center justify-center h-full space-y-8 px-6">
          <Link href="/" onClick={() => setIsMobileMenuOpen(false)} className="text-4xl font-display font-black text-brand-text hover:text-brand-primary transition-colors">Ana Sayfa</Link>
          <Link href="/kategoriler" onClick={() => setIsMobileMenuOpen(false)} className="text-4xl font-display font-black text-brand-text hover:text-brand-primary transition-colors">Kategoriler</Link>
          <Link href="/hakkimizda" onClick={() => setIsMobileMenuOpen(false)} className="text-4xl font-display font-black text-brand-text hover:text-brand-primary transition-colors">Hakkımızda</Link>
          <Link href="/iletisim" onClick={() => setIsMobileMenuOpen(false)} className="text-4xl font-display font-black text-brand-text hover:text-brand-primary transition-colors">İletişim</Link>
        </nav>
      </div>
    </>
  );
}
