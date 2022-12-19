# Restful_Encrypt-Decrypt_Operations
Rest servisleri üzerinden data Encryption/Decryption işlemlerini gerçekleştirecek projedir

TASARLADIĞIM MİMARİ

![image](https://user-images.githubusercontent.com/33135934/208492257-4c0bb0ff-4974-4b44-89c5-a160580de880.png)

![image](https://user-images.githubusercontent.com/33135934/208492349-e6a7bd65-e7b6-4e4c-bdb7-b2a932b06af7.png)

Client servisinden ilk olarak key ile pass değerlerini istemekteyim. Sistemle tanımlı olan kullanıcılar
Auth/Entities/DefinedUsers altında bulunmaktadır. Eğer böyle bir kullanıcı varsa AuthServisinde JWT
mekanizması kullanarak client a token dönüyorum. (Örnek key: key1, pass: pass1)

Sonrasında yapacağım işlemi ve işleme uğrayacak veriyi giriyorum. Token ile birlikte veriyi Crypto
Servisine gönderiyorum. Controller a gelmeden önce servisin middleware’ı requesti yakalıyor. Request
içerisinde bir token parametresi varsa Auth servisine gönderip token in gerçeliliği var mı yok mu diye
kontrol ediyorum. Token geçerliyse Crpyto servisinde Encrypt ve Decrypt işlemine tabii tutularak
clienta döndürmekteyim.


JWT(JSON Web Tokens) nasıl çalışır?
Üç kısımdan oluşur. Header, Payload(Veri), Signature(İmza)
Header kısmında token tipi ve imza için kullanılacak olan algorirmayı belirleriz.
Payload kullanıcı bilgileri hakkında bilgi verir. JWT doğrulaması yapılırken kullanırız.
Signature Bu kısmın oluşturulabilmesi için header, payload ve gizli anahtar(secret) gereklidir. Burada
kullandığımız gizli anahtar Header kısmında belirttiğimiz algoritma için kullanılır. Header ve Payload
kısımları bu gizli anahtar ile imzalanır

JWT Verify(Doğrulama)
Gelen tokenda Header(1. kısım) ve Payload(2. kısım) sunucumuzda bulunan gizli anahtar ile imzalanır
ve 3. kısım hesaplanır. Daha sonra bu oluşturulan imza(3. kısım) client tarafından gelen imza ile
karşılaştırılır. Eğer imzalar aynı ise token geçerli sayılır ve kullanıcıya erişim verilir.
KATMANLAR HAKKINDA BİLGİ
AuthService: JWT ile token üretip Validation yaptığım api
Client: Apilere bağlanan istemci.(Consol uygulamasıdır)
Core: Projenin her yerinde kullandığım dönüş değerlerini tek bir yerden yönetmek için kullandım.
CryptoService: RSA algoritmasını kullanarak Crypte işlemlerini gerçekleştirdiğim katmandır.
AuthService
Entities>DefinedUser da Sisteme tanımlı kullanıclar oluşturdum.
![image](https://user-images.githubusercontent.com/33135934/208492450-58a3ba92-af5d-440e-9421-b791ba38988e.png)


Interfaces> IJWTAuthenticationManager Jwt manager ı interface olarak tanımlama gerçekleştirdim.
Dönüş değeri Core katmanında oluşturduğum dönüş tipidir.

Services>JwtAuthenticationManager Token ürettiğim kısımdır.
Key ve Pass değerleri boş değil, aynı zamanda sistemde kayıtlıysa token üretme kısmına geçiyor.
JwtSecurityTokenHandler, SecurityTokenDescriptor nesneleri üretiyoruz. Token Descriptor ile tokenin
süresini, imzası için gerekli key gibi bilgilerini veriyoruz. CreateToken ile tokeni oluşturup WriteToken
ile tokenin son halini verdirmiş oluyoruz.
Descriptor da ki değerleri appsertting.json dan çekiyorum.

![image](https://user-images.githubusercontent.com/33135934/208492607-e10f91b8-f6ce-4ef6-bc35-b7593d6826a2.png)

Client

Client>ApiContect

Consol tarafından girilen kullanıcı bilgilerini token almak için Aut/Authenticate adresine gönderen ve
response alan metotdur.
Deryption/Encryption işleminde ilgili adrese datayla birlikte Headers kısmına token bilgisiyle
gönderen ve response alan metotdur.

Core Projede response olarak kullandığım classlar yer almaktadır.

Crypto Service Burada gelen requestleri karşılayacak middleware ve crypto işlemlerini yaptığım
kodları yazdım.

Middleware kısmını şöyle açıklayayım.

İlgili requestin header kısmındaki header ı alıp Bearer ile Auth servisinde kontrol ediyorum eğer
Status Code OK gelirse bir sonraki midlleware yi Invoke ediyorum.
Eğer token geçersizse hata mesajı yazdırıyorum.
Extensions kısmındaysa yazdığım middlewareyi IApplicatonBuilder türünde döndürmeye çalışıyorum.

Classes/CriptoService RSA algortimasına göre Encrypte/ Decrypte işlemlerini gerçekleştirmektedir.
Crypt işlemini önce interface de tanımladım sonrada ilgili sınıfta implemente ettim.
Startup dosyasında tanımlarken
AddSingletona göre yaptım. Bir kere nesne oluşturup aynı nesne üzerinden uygulamasını sağladım.

RSA ALGORİTMASI

![image](https://user-images.githubusercontent.com/33135934/208492965-5e522166-468e-4167-9ed3-76531451747e.png)


RSA algoritmasını internetten bulduğum resimle açıklamak istiyorum.
İki keyimiz var. Bunlar public ve private keydir. Veriyi şifreleyecek olan public key kullanır. Şifreyi
çözecek olansa private key kullanır. Böylece şifreyi sadece private keye sahip olanlar çözebilir.
