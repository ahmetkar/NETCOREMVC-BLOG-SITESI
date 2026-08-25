'use client';
import { useState } from 'react';
import { postIletisimMesaji, postBultenAbone, dogrulaBultenAboneliktenCik, postBultenAboneliktenCik } from '@/services/visitor/generalService';
import { ContactRequestDto } from '@/types/dto';

export default function ContactClient({ initialSettings }: { initialSettings: any }) {
  const [settings] = useState<any>(initialSettings);

  const [formData, setFormData] = useState({
    name: '',
    email: '',
    subject: '',
    tel: '',
    body: ''
  });
  const [status, setStatus] = useState({ type: '', message: '' });
  const [loading, setLoading] = useState(false);

  const contactEmail = settings?.contactEmail || 'info@blogger.com';
  const contactTitle = settings?.contactTitle || 'Merhaba de.';
  const contactDescription = settings?.contactDescription || 'Soru, öneri, proje fikirleri veya reklam iş birlikleri için bize yazın. Kahvemizi içerken mesajlarınızı okumaktan keyif alıyoruz.';

  const handleChange = (e: any) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: any) => {
    e.preventDefault();
    setLoading(true);
    setStatus({ type: '', message: '' });

    // Anti-bot honeypot check
    const form = e.target as HTMLFormElement;
    const honeypot = (form.elements.namedItem('hp_website') as HTMLInputElement)?.value;
    if (honeypot) {
      // Fake success for bots
      setStatus({ type: 'success', message: 'Mesajınız başarıyla gönderildi. Size en kısa sürede dönüş yapacağız.' });
      setFormData({ name: '', email: '', subject: '', tel: '', body: '' });
      setLoading(false);
      return;
    }

    try {
      const res = await postIletisimMesaji({
         Name: formData.name,
         Email: formData.email,
         Subject: formData.subject,
         Tel: formData.tel || '0000',
         Body: formData.body
      });
      if (res.success) {
         setStatus({ type: 'success', message: 'Mesajınız başarıyla gönderildi. Size en kısa sürede dönüş yapacağız.' });
         setFormData({ name: '', email: '', subject: '', tel: '', body: '' });
      } else {
         setStatus({ type: 'error', message: res.message || 'Gönderilemedi.' });
      }
    } catch (err) {
      setStatus({ type: 'error', message: 'Sunucuyla bağlantı kurulurken bir hata oluştu.' });
    }
    setLoading(false);
  };

  return (
    <div className="w-full relative min-h-screen">
      {/* Decorative Aurora Elements */}
      <div className="absolute top-[5%] right-[5%] w-[50%] h-[50%] rounded-full bg-brand-secondary/10 blur-[150px] pointer-events-none -z-10"></div>

      <div className="max-w-[1400px] mx-auto px-4 sm:px-6 lg:px-12 py-16 lg:py-24">
        <div className="flex flex-col lg:flex-row gap-16 lg:gap-24">
          
          {/* Contact Info (Left) */}
          <div className="lg:w-1/3 flex flex-col justify-center">
             <div className="mb-12">
               <span className="text-[11px] font-bold uppercase tracking-widest text-brand-primary mb-4 block">İletişim</span>
               <h1 className="text-5xl sm:text-6xl lg:text-7xl font-display font-black text-brand-text mb-6 tracking-tight">
                 {contactTitle}
               </h1>
               <p className="text-xl text-brand-muted font-medium">
                 {contactDescription}
               </p>
             </div>

             <div className="space-y-10">
                <div className="flex gap-4 items-start">
                   <div className="w-12 h-12 rounded-full bg-brand-primary/10 flex items-center justify-center shrink-0 text-brand-primary">
                     <svg className="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M3 8l7.89 5.26a2 2 0 002.22 0L21 8M5 19h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z" /></svg>
                   </div>
                   <div>
                     <p className="text-[11px] font-bold uppercase tracking-widest text-brand-muted mb-1">E-posta</p>
                     <a href={`mailto:${contactEmail}`} className="text-xl font-display font-bold text-brand-text hover:text-brand-primary transition-colors">{contactEmail}</a>
                   </div>
                </div>
             </div>
          </div>

          {/* Contact Form (Right) */}
          <div className="lg:w-2/3">
             <div className="neumorphic rounded-[2.5rem] p-8 md:p-12 lg:p-16">
               <form className="space-y-8" onSubmit={handleSubmit}>
                  {/* Anti-spam honeypot */}
                  <input 
                    type="text" 
                    name="hp_website" 
                    tabIndex={-1} 
                    autoComplete="off" 
                    className="hidden absolute w-0 h-0 opacity-0 pointer-events-none" 
                    aria-hidden="true" 
                  />
                  
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
                    <div className="space-y-3">
                      <label className="text-[11px] font-bold uppercase tracking-widest text-brand-muted ml-4">Adınız Soyadınız</label>
                      <input 
                        type="text" 
                        name="name"
                        value={formData.name}
                        onChange={handleChange}
                        required
                        className="w-full bg-white/50 border border-brand-border/50 rounded-full px-6 py-4 outline-none focus:border-brand-primary focus:bg-white focus:ring-4 focus:ring-brand-primary/10 transition-all font-medium placeholder:text-brand-muted/40 text-brand-text shadow-inner"
                        placeholder="Örn. John Doe"
                      />
                    </div>
                    <div className="space-y-3">
                      <label className="text-[11px] font-bold uppercase tracking-widest text-brand-muted ml-4">E-posta Adresiniz</label>
                      <input 
                        type="email" 
                        name="email"
                        value={formData.email}
                        onChange={handleChange}
                        required
                        className="w-full bg-white/50 border border-brand-border/50 rounded-full px-6 py-4 outline-none focus:border-brand-primary focus:bg-white focus:ring-4 focus:ring-brand-primary/10 transition-all font-medium placeholder:text-brand-muted/40 text-brand-text shadow-inner"
                        placeholder="john@example.com"
                      />
                    </div>
                  </div>
                  
                  <div className="space-y-3">
                    <label className="text-[11px] font-bold uppercase tracking-widest text-brand-muted ml-4">Konu</label>
                    <input 
                      type="text" 
                      name="subject"
                      value={formData.subject}
                      onChange={handleChange}
                      required
                      className="w-full bg-white/50 border border-brand-border/50 rounded-full px-6 py-4 outline-none focus:border-brand-primary focus:bg-white focus:ring-4 focus:ring-brand-primary/10 transition-all font-medium placeholder:text-brand-muted/40 text-brand-text shadow-inner"
                      placeholder="Mesajınızın konusu nedir?"
                    />
                  </div>

                  <div className="space-y-3">
                    <label className="text-[11px] font-bold uppercase tracking-widest text-brand-muted ml-4">Mesajınız</label>
                    <textarea 
                      rows={6}
                      name="body"
                      value={formData.body}
                      onChange={handleChange}
                      required
                      className="w-full bg-white/50 border border-brand-border/50 rounded-3xl px-6 py-4 outline-none focus:border-brand-primary focus:bg-white focus:ring-4 focus:ring-brand-primary/10 transition-all font-medium placeholder:text-brand-muted/40 text-brand-text shadow-inner resize-none"
                      placeholder="Bize detaylardan bahsedin..."
                    ></textarea>
                  </div>

                  {status.message && (
                    <div className={`p-4 rounded-2xl flex items-center gap-3 text-sm font-bold ${status.type === 'success' ? 'bg-green-50 text-green-700 border border-green-200' : 'bg-red-50 text-red-700 border border-red-200'}`}>
                      {status.type === 'success' ? (
                        <svg className="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z" /></svg>
                      ) : (
                        <svg className="w-5 h-5" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" /></svg>
                      )}
                      {status.message}
                    </div>
                  )}

                  <button disabled={loading} type="submit" className="w-full sm:w-auto px-10 py-5 bg-brand-primary text-white text-[13px] font-bold tracking-widest uppercase rounded-full hover:bg-brand-primary/90 hover:shadow-xl hover:shadow-brand-primary/30 transition-all transform hover:-translate-y-1 disabled:opacity-50 disabled:transform-none mt-4">
                    {loading ? 'Gönderiliyor...' : 'Mesajı Gönder'}
                  </button>
               </form>
             </div>
          </div>

        </div>
      </div>
    </div>
  );
}
