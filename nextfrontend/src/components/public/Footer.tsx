'use client';
import Link from 'next/link';
import { useLayoutData } from '@/providers/LayoutDataProvider';
import { postBultenAbone } from '@/services/visitor/generalService';
import { useState } from 'react';
import { getImageUrl } from '@/lib/image-url';

export default function Footer() {
  const layoutData = useLayoutData();
  const settings = layoutData?.settings || {};
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState('');

  const handleSubscribe = async (e: any) => {
    e.preventDefault();
    setLoading(true);
    setMessage('');
    const form = e.target as HTMLFormElement;
    const honeypot = (form.elements.namedItem('hp_username') as HTMLInputElement)?.value;
    if (honeypot) {
      // Bot detected - simulate success without sending request
      setMessage('Aboneliğiniz alındı!');
      setLoading(false);
      form.reset();
      return;
    }

    const email = (form.elements.namedItem('email') as HTMLInputElement).value;
    try {
      const res = await postBultenAbone(email);
      setMessage(res.message || 'Abone oldunuz!');
      form.reset();
    } catch(err) {
      setMessage('Abonelik işlemi başarısız oldu.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <footer className="relative bg-brand-dark-bg text-brand-dark-text overflow-hidden pt-24 pb-12 mt-20">
      
      {/* Background Aurora Effect */}
      <div className="absolute top-0 left-0 w-full h-full overflow-hidden pointer-events-none z-0 opacity-40">
        <div className="absolute -top-[20%] -left-[10%] w-[50%] h-[50%] rounded-full bg-brand-primary/20 blur-[120px]"></div>
        <div className="absolute top-[40%] -right-[10%] w-[40%] h-[40%] rounded-full bg-brand-secondary/20 blur-[100px]"></div>
      </div>

      <div className="relative z-10 max-w-[1400px] mx-auto px-4 sm:px-6 lg:px-12">
        
        {/* Newsletter Section - Glass Card */}
        <div id="newsletter" className="glass-dark rounded-3xl p-10 md:p-16 mb-24">
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-16 items-center">
            <div>
              <h2 className="text-4xl sm:text-5xl lg:text-6xl font-display font-black leading-tight tracking-tight mb-6 bg-clip-text text-transparent bg-gradient-to-r from-white to-white/70">
                Geleceği Kaçırmayın.
              </h2>
              <p className="text-lg text-brand-dark-muted max-w-md font-sans">
                Haftalık bültenimize abone olun, dijital dünyanın en önemli gelişmelerini herkesten önce öğrenin. Spamsiz, sadece nitelikli içerik.
              </p>
            </div>
            <div className="flex flex-col justify-center">
              <form onSubmit={handleSubscribe} className="relative flex items-center bg-white/5 rounded-full p-2 border border-white/10 focus-within:border-brand-primary/50 focus-within:bg-white/10 transition-all">
                {/* Anti-spam honeypot */}
                <input 
                  type="text" 
                  name="hp_username" 
                  tabIndex={-1} 
                  autoComplete="off" 
                  className="hidden absolute w-0 h-0 opacity-0 pointer-events-none" 
                  aria-hidden="true" 
                />
                <input 
                  type="email" 
                  name="email"
                  aria-label="Bülten e-posta adresi"
                  placeholder="E-posta adresiniz..." 
                  className="w-full bg-transparent border-none outline-none text-base md:text-lg font-sans text-white placeholder:text-brand-dark-muted px-6"
                  required
                />
                <button 
                  type="submit" 
                  disabled={loading}
                  aria-label="Bültene Abone Ol"
                  className="bg-brand-primary hover:bg-brand-primary/90 text-white text-sm font-bold uppercase tracking-wider rounded-full px-8 py-4 transition-all flex-shrink-0 disabled:opacity-50"
                >
                  {loading ? 'Bekleyin...' : 'Abone Ol'}
                </button>
              </form>
              {message && <p className="mt-4 text-sm text-green-400 text-center font-medium">{message}</p>}
            </div>
          </div>
        </div>

        {/* Footer Links & Info */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-12 pt-12 border-t border-brand-dark-border/50">
          
          <div className="md:col-span-2">
            <Link href="/" className="inline-block mb-6">
              {settings?.footerLogo?.fileName ? (
                <img src={getImageUrl(settings.footerLogo.fileName)} alt={settings.siteTitle || "Logo"} className="h-12 w-auto object-contain transition-transform group-hover:scale-105 duration-300" />
              ) : settings?.logoImage?.fileName ? (
                <img src={getImageUrl(settings.logoImage.fileName)} alt={settings.siteTitle || "Logo"} className="h-12 w-auto object-contain invert mix-blend-screen opacity-90" />
              ) : (
                <span className="text-3xl font-display font-black tracking-tight text-white">
                  {settings.siteTitle || 'Blogger.'}
                </span>
              )}
            </Link>
            <p className="text-brand-dark-muted text-sm max-w-sm leading-relaxed">
              Fikirlerin gelecekle buluştuğu yer. Teknoloji, tasarım ve kültür üzerine premium bir yayın platformu. Hayal et, oku, paylaş.
            </p>
          </div>

          <div>
            <h4 className="text-[11px] font-bold uppercase tracking-widest text-brand-dark-muted mb-6">Keşfet</h4>
            <ul className="space-y-4">
              <li><Link href="/" className="text-sm font-medium hover:text-brand-primary transition-colors">Ana Sayfa</Link></li>
              <li><Link href="/kategoriler" className="text-sm font-medium hover:text-brand-primary transition-colors">Kategoriler</Link></li>
              <li><Link href="/hakkimizda" className="text-sm font-medium hover:text-brand-primary transition-colors">Hakkımızda</Link></li>
              <li><Link href="/iletisim" className="text-sm font-medium hover:text-brand-primary transition-colors">İletişim</Link></li>
            </ul>
          </div>

          <div>
            <h4 className="text-[11px] font-bold uppercase tracking-widest text-brand-dark-muted mb-6">Sosyal Medya</h4>
            <ul className="space-y-4">
              {settings.facebookUrl && <li><a href={settings.facebookUrl} className="text-sm font-medium hover:text-brand-primary transition-colors">Facebook</a></li>}
              {settings.twitterUrl && <li><a href={settings.twitterUrl} className="text-sm font-medium hover:text-brand-primary transition-colors">Twitter</a></li>}
              {settings.instagramUrl && <li><a href={settings.instagramUrl} className="text-sm font-medium hover:text-brand-primary transition-colors">Instagram</a></li>}
              {settings.linkedinUrl && <li><a href={settings.linkedinUrl} className="text-sm font-medium hover:text-brand-primary transition-colors">LinkedIn</a></li>}
              {settings.youtubeUrl && <li><a href={settings.youtubeUrl} className="text-sm font-medium hover:text-brand-primary transition-colors">YouTube</a></li>}
            </ul>
          </div>

        </div>

        <div className="flex flex-col sm:flex-row justify-between items-center mt-20 pt-8 border-t border-brand-dark-border/50 text-xs font-medium text-brand-dark-muted">
          <p>© 2026 {settings.siteTitle || 'Blogger.'}. Tüm Hakları Saklıdır.</p>
          <div className="flex space-x-6 mt-4 sm:mt-0">
            <Link href="#" className="hover:text-white transition-colors">Gizlilik Politikası</Link>
            <Link href="#" className="hover:text-white transition-colors">Kullanım Şartları</Link>
          </div>
        </div>
        
      </div>
    </footer>
  );
}
