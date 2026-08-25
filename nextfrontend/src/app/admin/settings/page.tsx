'use client';
import { useEffect, useState } from 'react';
import { Form, Input, Button, Upload, Card, Select, Switch, Row, Col, Spin, App } from 'antd';
import { UploadOutlined, SaveOutlined } from '@ant-design/icons';
import { fetchAdminAyarlar, updateAdminAyarlar } from '@/services/admin/settingSubscriberService';
import { fetchKategoriler } from '@/services/visitor/categoryService';
import { fetchMakalelerMinimal } from '@/services/visitor/articleService';
import { SettingsDto, CategoryDto, ArticleDto } from '@/types/dto';
import { getImageUrl } from '@/lib/image-url';

const { TextArea } = Input;
const { Option } = Select;

export default function AdminSettings() {
  const [form] = Form.useForm();
  const { message } = App.useApp();
  
  const [loading, setLoading] = useState(false);
  const [fetching, setFetching] = useState(true);
  const [categories, setCategories] = useState<CategoryDto[]>([]);
  const [articles, setArticles] = useState<ArticleDto[]>([]);

  // File states
  const [logoFile, setLogoFile] = useState<any[]>([]);
  const [footerLogoFile, setFooterLogoFile] = useState<any[]>([]);
  const [adminLogoFile, setAdminLogoFile] = useState<any[]>([]);
  
  // existing images preview
  const [existingImages, setExistingImages] = useState<any>({});

  async function fetchData() {
    setFetching(true);
    try {
      const [settingsData, categoriesData, articlesData] = await Promise.all([
        fetchAdminAyarlar(),
        fetchKategoriler().catch(() => null),
        fetchMakalelerMinimal().catch(() => null)
      ]);
      
      const data = settingsData;
      
      form.setFieldsValue({
        siteTitle: data.siteTitle,
        footerDescription: data.footerDescription,
        facebookUrl: data.facebookUrl,
        instagramUrl: data.instagramUrl,
        twitterurl: data.twitterurl,
        youtubeurl: data.youtubeurl,
        isAIEnabled: data.isAIEnabled,
        category1Id: data.category1Id,
        category2Id: data.category2Id,
        category3Id: data.category3Id,
        category4Id: data.category4Id,
        category5Id: data.category5Id,
        heroArticleId: data.heroArticleId,
        featuredArticle1Id: data.featuredArticle1Id,
        featuredArticle2Id: data.featuredArticle2Id,
        aboutUsTitle: data.aboutUsTitle,
        aboutUsDescription: data.aboutUsDescription,
        aboutUsSectionTitle: data.aboutUsSectionTitle,
        aboutUsSectionDescription: data.aboutUsSectionDescription,
        aboutUsCard1Title: data.aboutUsCard1Title,
        aboutUsCard1Description: data.aboutUsCard1Description,
        aboutUsCard2Title: data.aboutUsCard2Title,
        aboutUsCard2Description: data.aboutUsCard2Description,
        aboutUsCard3Title: data.aboutUsCard3Title,
        aboutUsCard3Description: data.aboutUsCard3Description,
        contactEmail: data.contactEmail,
        contactTitle: data.contactTitle,
        contactDescription: data.contactDescription,
      });
      
      setExistingImages({
        logo: data.logoImage?.fileName,
        footerLogo: data.footerLogo?.fileName,
        adminLogo: data.adminPanelLogo?.fileName
      });
      
      if (categoriesData) {
        setCategories(categoriesData);
      }
      if (articlesData) {
        setArticles(articlesData);
      }

    } catch (error) {
      console.error(error);
      message.error('Ayarlar yüklenemedi.');
    } finally {
      setFetching(false);
    }
  }

  useEffect(() => {
    fetchData();
  }, []);

  const onFinishSettings = async (values: any) => {
    setLoading(true);
    try {
      const formData = new FormData();
      
      // Text inputs
      Object.keys(values).forEach(key => {
        if(values[key] !== undefined && values[key] !== null) {
           formData.append(key, values[key]);
        }
      });
      
      // Files
      if (logoFile.length > 0 && logoFile[0].originFileObj) {
        formData.append('LogoFile', logoFile[0].originFileObj);
      }
      if (footerLogoFile.length > 0 && footerLogoFile[0].originFileObj) {
        formData.append('FooterLogoFile', footerLogoFile[0].originFileObj);
      }
      if (adminLogoFile.length > 0 && adminLogoFile[0].originFileObj) {
        formData.append('AdminPanelLogoFile', adminLogoFile[0].originFileObj);
      }

      const res = await updateAdminAyarlar(formData);
      
      message.success(res?.message || 'Ayarlar başarıyla güncellendi.');
    } catch (error: any) {
      message.error(error.response?.data?.message || 'Ayarlar güncellenirken hata oluştu.');
    } finally {
      setLoading(false);
    }
  };

  const createUploadProps = (fileList: any[], setFileList: any) => ({
    onRemove: () => setFileList([]),
    beforeUpload: (file: any) => {
      setFileList([file]);
      return false; 
    },
    fileList,
    maxCount: 1
  });

  return (
    <div>
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold">Site Ayarları</h1>
      </div>

      <Card className="shadow-sm">
        <Spin spinning={fetching}>
          <Form
            form={form}
            layout="vertical"
            onFinish={onFinishSettings}
            initialValues={{ isAIEnabled: true }}
          >
            {/* ---- GENEL AYARLAR ---- */}
            <h3 className="text-lg font-semibold mb-4 border-b pb-2">Genel Ayarlar</h3>
            <Row gutter={24}>
                <Col xs={24} lg={12}>
                    <Form.Item name="siteTitle" label="Site Başlığı">
                        <Input size="large" />
                    </Form.Item>
                    
                    <Form.Item name="footerDescription" label="Footer Açıklaması">
                        <TextArea rows={4} />
                    </Form.Item>

                    <Form.Item name="isAIEnabled" label="Yapay Zeka Asistanı Aktif mi?" valuePropName="checked">
                        <Switch />
                    </Form.Item>
                </Col>
            </Row>

            {/* ---- MANŞET VE ÖNE ÇIKANLAR ---- */}
            <Row gutter={24} className="mt-8">
                <Col xs={24}>
                    <h3 className="text-lg font-semibold mb-4 border-b pb-2">Ana Sayfa Manşet ve Öne Çıkanlar</h3>
                    <p className="text-gray-500 mb-4 text-sm">Ana sayfada en üstte gösterilecek ana manşeti ve yanındaki öne çıkan 2 makaleyi seçin.</p>
                    <Row gutter={16}>
                        <Col xs={24} sm={12} md={8}>
                            <Form.Item name="heroArticleId" label="Manşet Makalesi">
                                <Select size="large" allowClear placeholder="Varsayılan: Son Eklenen">
                                    {articles.map(a => (
                                        <Option key={a.id} value={a.id}>{a.title}</Option>
                                    ))}
                                </Select>
                            </Form.Item>
                        </Col>
                        <Col xs={24} sm={12} md={8}>
                            <Form.Item name="featuredArticle1Id" label="1. Öne Çıkan Makale">
                                <Select size="large" allowClear placeholder="Varsayılan: Otomatik">
                                    {articles.map(a => (
                                        <Option key={a.id} value={a.id}>{a.title}</Option>
                                    ))}
                                </Select>
                            </Form.Item>
                        </Col>
                        <Col xs={24} sm={12} md={8}>
                            <Form.Item name="featuredArticle2Id" label="2. Öne Çıkan Makale">
                                <Select size="large" allowClear placeholder="Varsayılan: Otomatik">
                                    {articles.map(a => (
                                        <Option key={a.id} value={a.id}>{a.title}</Option>
                                    ))}
                                </Select>
                            </Form.Item>
                        </Col>
                    </Row>
                </Col>
            </Row>

            {/* ---- KATEGORİ ALANLARI ---- */}
            <Row gutter={24} className="mt-8">
                <Col xs={24}>
                    <h3 className="text-lg font-semibold mb-4 border-b pb-2">Ana Sayfa Kategori Alanları</h3>
                    <p className="text-gray-500 mb-4 text-sm">Ana sayfada listelenecek kategorileri belirleyin.</p>
                    <Row gutter={16}>
                        {[1, 2, 3, 4, 5].map((num) => (
                            <Col xs={24} sm={12} md={8} key={`category-${num}`}>
                                <Form.Item name={`category${num}Id`} label={`${num}. Kategori`}>
                                    <Select size="large" allowClear placeholder="Kategori Seçin">
                                        {categories.map(c => (
                                            <Option key={c.id} value={c.id}>{c.name}</Option>
                                        ))}
                                    </Select>
                                </Form.Item>
                            </Col>
                        ))}
                    </Row>
                </Col>
            </Row>

            {/* ---- LOGOLAR ---- */}
            <Row gutter={24} className="mt-8">
                <Col xs={24} lg={12}>
                    <h3 className="text-lg font-semibold mb-4 border-b pb-2">Logolar (İsteğe Bağlı)</h3>
                    
                    <Form.Item label="Ana Logo">
                        {existingImages.logo && (
                            <img src={getImageUrl(existingImages.logo)} alt="logo" className="h-12 mb-2 object-contain" />
                        )}
                        <Upload {...createUploadProps(logoFile, setLogoFile)} accept="image/*">
                        <Button icon={<UploadOutlined />}>Resim Seç</Button>
                        </Upload>
                    </Form.Item>

                    <Form.Item label="Footer Logosu">
                        {existingImages.footerLogo && (
                            <img src={getImageUrl(existingImages.footerLogo)} alt="footer" className="h-12 mb-2 object-contain bg-gray-200 p-2 rounded" />
                        )}
                        <Upload {...createUploadProps(footerLogoFile, setFooterLogoFile)} accept="image/*">
                        <Button icon={<UploadOutlined />}>Resim Seç</Button>
                        </Upload>
                    </Form.Item>

                    <Form.Item label="Admin Panel Logosu">
                        {existingImages.adminLogo && (
                            <img src={getImageUrl(existingImages.adminLogo)} alt="admin" className="h-12 mb-2 object-contain" />
                        )}
                        <Upload {...createUploadProps(adminLogoFile, setAdminLogoFile)} accept="image/*">
                        <Button icon={<UploadOutlined />}>Resim Seç</Button>
                        </Upload>
                    </Form.Item>
                </Col>

                {/* Sosyal Medya */}
                <Col xs={24} lg={12}>
                    <h3 className="text-lg font-semibold mb-4 border-b pb-2">Sosyal Medya Linkleri</h3>
                    
                    <Form.Item name="facebookUrl" label="Facebook URL">
                        <Input size="large" placeholder="https://facebook.com/..." />
                    </Form.Item>
                    <Form.Item name="instagramUrl" label="Instagram URL">
                        <Input size="large" placeholder="https://instagram.com/..." />
                    </Form.Item>
                    <Form.Item name="twitterurl" label="Twitter (X) URL">
                        <Input size="large" placeholder="https://twitter.com/..." />
                    </Form.Item>
                    <Form.Item name="youtubeurl" label="YouTube URL">
                        <Input size="large" placeholder="https://youtube.com/..." />
                    </Form.Item>
                </Col>
            </Row>

            {/* ---- HAKKIMIZDA AYARLARI ---- */}
            <Row gutter={24} className="mt-8">
                <Col xs={24}>
                    <h3 className="text-lg font-semibold mb-4 border-b pb-2">Hakkımızda Sayfası</h3>
                    <p className="text-gray-500 mb-4 text-sm">Hakkımızda sayfasında gösterilecek başlık, açıklama ve kart bilgilerini düzenleyin.</p>
                    <Row gutter={24} className="mb-4">
                        <Col xs={24} lg={12}>
                            <Form.Item name="aboutUsTitle" label="Hero Başlığı">
                                <Input size="large" placeholder="Geleceği şekillendiren fikirler." />
                            </Form.Item>
                        </Col>
                        <Col xs={24} lg={12}>
                            <Form.Item name="aboutUsDescription" label="Hero Açıklaması">
                                <TextArea rows={3} placeholder="Kısa bir açıklama yazın..." />
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={24} className="mb-4">
                        <Col xs={24} lg={12}>
                            <Form.Item name="aboutUsSectionTitle" label="Kart Bölümü Başlığı">
                                <Input size="large" placeholder="Sadece olanı biteni değil, arkasındaki nedenleri inceliyoruz." />
                            </Form.Item>
                        </Col>
                        <Col xs={24} lg={12}>
                            <Form.Item name="aboutUsSectionDescription" label="Kart Bölümü Açıklaması">
                                <TextArea rows={3} placeholder="Modern dünyada bilginin hızı kadar doğruluğu..." />
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={24}>
                        {[1, 2, 3].map((num) => (
                            <Col xs={24} md={8} key={`card-${num}`}>
                                <div className="border rounded-lg p-4 mb-4">
                                    <h4 className="font-medium mb-3">Kart {num}</h4>
                                    <Form.Item name={`aboutUsCard${num}Title`} label="Başlık">
                                        <Input placeholder={`Kart ${num} başlığı`} />
                                    </Form.Item>
                                    <Form.Item name={`aboutUsCard${num}Description`} label="Açıklama">
                                        <TextArea rows={3} placeholder={`Kart ${num} açıklaması`} />
                                    </Form.Item>
                                </div>
                            </Col>
                        ))}
                    </Row>
                </Col>
            </Row>

            {/* ---- İLETİŞİM AYARLARI ---- */}
            <Row gutter={24} className="mt-8">
                <Col xs={24}>
                    <h3 className="text-lg font-semibold mb-4 border-b pb-2">İletişim Sayfası</h3>
                    <p className="text-gray-500 mb-4 text-sm">İletişim sayfasında gösterilecek başlık, açıklama ve e-posta adresini düzenleyin.</p>
                    <Row gutter={24}>
                        <Col xs={24} md={8}>
                            <Form.Item name="contactEmail" label="İletişim E-posta">
                                <Input size="large" placeholder="info@siteniz.com" />
                            </Form.Item>
                        </Col>
                        <Col xs={24} md={8}>
                            <Form.Item name="contactTitle" label="İletişim Başlığı">
                                <Input size="large" placeholder="Merhaba de." />
                            </Form.Item>
                        </Col>
                        <Col xs={24} md={8}>
                            <Form.Item name="contactDescription" label="İletişim Açıklaması">
                                <TextArea rows={3} placeholder="Soru, öneri, proje fikirleri veya reklam iş birlikleri için bize yazın..." />
                            </Form.Item>
                        </Col>
                    </Row>
                </Col>
            </Row>

            <div className="flex justify-end mt-8 border-t pt-6">
                <Button type="primary" htmlType="submit" size="large" icon={<SaveOutlined />} loading={loading}>
                Ayarları Kaydet
                </Button>
            </div>
          </Form>
        </Spin>
      </Card>
    </div>
  );
}
