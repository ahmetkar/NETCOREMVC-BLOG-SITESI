export interface ImageFile {
  id?: string;
  fileName?: string;
}

export interface UserDto {
  id?: string;
  firstName?: string;
  lastName?: string;
  email?: string;
  userName?: string;
  biography?: string;
  image?: ImageFile;
  role?: string;
}

export interface CategoryDto {
  id?: string;
  name?: string;
  slug?: string;
}

export interface CommentDto {
  id?: string;
  articleId?: string;
  name?: string;
  email?: string;
  text?: string;
  commentText?: string;
  createdDate?: string;
  isDeleted?: boolean;
  isAprroved?: boolean;
}

export interface ArticleDto {
  id: string;
  title: string;
  description?: string;
  content?: string;
  slug?: string;
  createdDate: string;
  modifiedDate?: string;
  viewCount?: number;
  category?: CategoryDto;
  user?: UserDto;
  image?: ImageFile;
  comments?: CommentDto[];
}
export interface ArticleDetailClientProps {
  article: ArticleDto;
  mayLikeArticles?: ArticleDto[];
}


export interface SettingsDto {
  siteTitle?: string;
  footerDescription?: string;
  contactEmail?: string;
  contactTitle?: string;
  contactDescription?: string;
  facebookUrl?: string;
  twitterUrl?: string;
  twitterurl?: string;
  instagramUrl?: string;
  linkedinUrl?: string;
  youtubeUrl?: string;
  youtubeurl?: string;
  isAIEnabled?: boolean;
  aboutUsTitle?: string;
  aboutUsDescription?: string;
  aboutUsSectionTitle?: string;
  aboutUsSectionDescription?: string;
  aboutUsCard1Title?: string;
  aboutUsCard1Description?: string;
  aboutUsCard2Title?: string;
  aboutUsCard2Description?: string;
  aboutUsCard3Title?: string;
  aboutUsCard3Description?: string;
  logoImage?: ImageFile;
  footerLogo?: ImageFile;
  adminPanelLogo?: ImageFile;
  category1Id?: string;
  category2Id?: string;
  category3Id?: string;
  category4Id?: string;
  category5Id?: string;
  heroArticleId?: string;
  featuredArticle1Id?: string;
  featuredArticle2Id?: string;
}

export interface MessageDto {
  id: string;
  name: string;
  email: string;
  subject: string;
  tel?: string;
  body: string;
  isRead: boolean;
  createdDate: string;
}

export interface SubscriberDto {
  id: string;
  email: string;
  createdDate: string;
  ipAddress?: string;
  isActive?: boolean;
}

export interface MediaDto {
  id: string;
  fileName: string;
  type: string;
  createdDate: string;
  isDeleted?: boolean;
}

export interface LayoutDataDto {
  settings: SettingsDto;
  navCategories: CategoryDto[];
  footerCategories: CategoryDto[];
  categories: CategoryDto[];
  topArticles: ArticleDto[];
  sidebarTopArticles: ArticleDto[];
  sidebarMostCommented: ArticleDto[];
  heroArticle?: ArticleDto;
  featuredArticles?: ArticleDto[];
}

export interface ContactRequestDto {
  Name: string;
  Email: string;
  Subject: string;
  Tel?: string;
  Body: string;
}

export interface LoginRequestDto {
  email: string;
  password: string;
}

export interface LoginResponseDto {
  token: string;
  expiration: string;
}
