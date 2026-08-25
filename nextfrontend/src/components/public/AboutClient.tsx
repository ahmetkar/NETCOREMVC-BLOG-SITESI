'use client';
import { useState } from 'react';

export default function AboutClient({ initialSettings }: { initialSettings: any }) {
  const [settings] = useState<any>(initialSettings);

  const aboutUsTitle = settings?.aboutUsTitle || 'Geleceği şekillendiren fikirler.';
  const aboutUsDescription = settings?.aboutUsDescription || 'Dijital dünyanın nabzını tutan, derinlemesine analizler ve vizyoner perspektifler sunan modern yayın platformuyuz. Sadece haberi değil, arkasındaki hikayeyi anlatıyoruz.';
  const sectionTitle = settings?.aboutUsSectionTitle || 'Sadece olanı biteni değil, arkasındaki nedenleri inceliyoruz.';
  const sectionDescription = settings?.aboutUsSectionDescription || 'Modern dünyada bilginin hızı kadar doğruluğu ve sunumu da önemlidir. İçerik tüketimini sıradan bir eylemden çıkarıp, keyifli bir deneyime dönüştürüyoruz.';
  
  const cards = [
    {
      num: '01',
      title: settings?.aboutUsCard1Title || 'Objektiflik',
      description: settings?.aboutUsCard1Description || 'Olaylara farklı açılardan bakar, tarafsız bir perspektif sunarız. Gerçeği filtrelemeden, en saf haliyle aktarmayı hedefleriz.',
      colorClass: 'brand-primary'
    },
    {
      num: '02',
      title: settings?.aboutUsCard2Title || 'Derinlik',
      description: settings?.aboutUsCard2Description || 'Yüzeysel içerikler yerine detaylı analizler ve araştırmalar üretiriz. Okurlarımızın ufkunu açacak detayları bulur çıkarırız.',
      colorClass: 'brand-secondary'
    },
    {
      num: '03',
      title: settings?.aboutUsCard3Title || 'Estetik',
      description: settings?.aboutUsCard3Description || 'İçeriğin sunumunun da kendisi kadar önemli olduğuna inanırız. Kusursuz bir okuma deneyimi için tasarımı her zaman ön planda tutarız.',
      colorClass: 'brand-text'
    }
  ];

  return (
    <div className="w-full relative min-h-screen">
      {/* Decorative Aurora Elements */}
      <div className="absolute top-[10%] left-[5%] w-[40%] h-[40%] rounded-full bg-brand-primary/10 blur-[120px] pointer-events-none -z-10"></div>
      <div className="absolute top-[40%] right-[10%] w-[30%] h-[30%] rounded-full bg-brand-secondary/10 blur-[100px] pointer-events-none -z-10"></div>

      {/* Hero Section */}
      <div className="max-w-[1400px] mx-auto px-4 sm:px-6 lg:px-12 py-20 lg:py-32 flex flex-col lg:flex-row gap-16 items-center">
        <div className="lg:w-1/2 flex flex-col justify-center">
           <div className="flex items-center gap-3 mb-8">
             <div className="w-12 h-1 bg-brand-primary"></div>
             <span className="text-[11px] font-bold uppercase tracking-widest text-brand-primary">Hakkımızda</span>
           </div>
           <h1 className="text-5xl sm:text-6xl lg:text-7xl xl:text-8xl font-display font-black text-brand-text mb-8 leading-[1.1] tracking-tight">
             {aboutUsTitle}
           </h1>
           <p className="text-xl text-brand-muted font-medium max-w-2xl leading-relaxed">
             {aboutUsDescription}
           </p>
        </div>
        <div className="lg:w-1/2 relative w-full aspect-square md:aspect-[4/3] lg:aspect-square">
           <div className="absolute inset-0 bg-gradient-to-tr from-brand-primary/20 to-brand-secondary/20 rounded-[3rem] transform rotate-3 scale-105 -z-10"></div>
           <img 
             src="https://images.unsplash.com/photo-1522071820081-009f0129c71c?ixlib=rb-4.0.3&auto=format&fit=crop&w=1600&q=80" 
             alt="Team Collaboration" 
             className="w-full h-full object-cover rounded-[3rem] shadow-2xl"
           />
        </div>
      </div>

      {/* Core Values (Cards) */}
      <div className="bg-brand-surface py-24 relative overflow-hidden">
        {/* Abstract shapes */}
        <div className="absolute -top-40 -right-40 w-96 h-96 bg-brand-bg rounded-full opacity-50 blur-3xl"></div>
        <div className="absolute -bottom-40 -left-40 w-96 h-96 bg-brand-primary/5 rounded-full opacity-50 blur-3xl"></div>
        
        <div className="max-w-[1400px] mx-auto px-4 sm:px-6 lg:px-12 relative z-10">
          <div className="text-center max-w-3xl mx-auto mb-20">
            <h2 className="text-4xl md:text-5xl font-display font-black text-brand-text mb-6 leading-tight">
              {sectionTitle}
            </h2>
            <p className="text-lg text-brand-muted font-medium">
              {sectionDescription}
            </p>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
             {cards.map((card, index) => (
                <div key={index} className={`glass rounded-3xl p-10 hover:shadow-2xl hover:shadow-brand-primary/10 transition-all duration-500 transform hover:-translate-y-2 ${index === 1 ? 'mt-0 md:mt-12' : index === 2 ? 'mt-0 md:mt-24' : ''}`}>
                   <div className={`w-16 h-16 rounded-2xl ${index === 0 ? 'bg-brand-primary/10' : index === 1 ? 'bg-brand-secondary/10' : 'bg-brand-text/5'} flex items-center justify-center mb-8`}>
                     <span className={`text-3xl font-display font-black ${index === 0 ? 'text-brand-primary' : index === 1 ? 'text-brand-secondary' : 'text-brand-text'}`}>{card.num}</span>
                   </div>
                   <h3 className="text-2xl font-display font-bold text-brand-text mb-4">{card.title}</h3>
                   <p className="text-brand-muted leading-relaxed font-medium">
                     {card.description}
                   </p>
                </div>
             ))}
          </div>
        </div>
      </div>

    </div>
  );
}
