# AspNetCore.JWT
Net 8 ile JWT

<h2>JSON Web Token (JWT) Nedir ?</h2>

<p>
    JSON Web Token (JWT), iletişim yapan birimler arasındaki veri alışverişinin güvenli bir şekilde sağlanması için bir JSON nesnesi (token) kullanarak daha kompakt ve bilginin kendi kendini betimlediği bir yol sunan endüstri standardıdır (RFC 7519). Oluşturulan token, dijital olarak imzalandığı için doğrulanabilir ve güvenilirdir. Bir JWT, HMAC algoritması ve gizli bir kelime kullanılarak imzalanabilir. İmzalama sürecinde HMAC yerine RSA algoritmasından yararlanılarak açık ve gizli anahtar ikililerinin kullanılması da sağlanabilir.
</p>

<h2>Ne Zaman JWT Kullanmalıyız ?</h2>

<ol type="a">
    <li>
        <b>Kimliklendirme:</b> Aslında en temel kullanım senaryosu budur. Kullanıcının başarılı yaptığı giriş işlemi sonrasında gerçekleştireceği her istek JWT’yi içerir. Bu sayede kullanıcının hangi kaynaklara/web sayfalarına erişeceği bu token bilgisi ile kontrol edilir. Farklı domain’ler arasında kolayca token değiş-tokuşunu sağladığı için, tek seferlik giriş (single sign on) senaryolarında yoğun olarak kullanılmaktadır.
    </li>
    <li>
        <b>Bilgi değiş-tokuşu:</b> JWT’ler dijital olarak imzalanabildiği için bilginin iletişim yapan birimler arası güvenli bir şekilde gerçekleşebilmesi için iyi bir yöntem sunar. Örneğin açık/gizli anahtar ikilileri kullanıldığında bilgiyi gönderen kişinin gerçekte kim olduğunu söylemek mümkündür. Buna ek olarak, JWT’deki header ve payload kısımları dahil edilerek oluşturulan imza sayesinde gelen bilginin değiştirilip/değiştirilmediği kolayca doğrulanabilir.
    </li>
</ol>

<h2>JWT Yapısı</h2>

<ol type="1">
    <li>Header</li>
    <li>Payload</li>
    <li>Signature</li>
</ol>

<h3>Header(Başlık)</h3>

<p>Başlık genellikle 2 parçadan oluşur</p>

<ol type="1">
    <li><b>typ:</b> Token'ın türünü belirler. "JWT" değerine sahiptir.</li>
    <li><b>alg:</b> Kullanılan özet (hash) algoritmasını gösterir. HMAC, HS256, SHA256 veya RSA gibi değerler alabilir.</li>
</ol>

<h3>Payload(Yük)</h3>

<p>
    Token'ın ikinci parçası olan payload, ilgli claim'leri içerir. Claim'ler kullanıcı hakkında bilgiler sunan ifadelerdir. Ayrıca metadata bilgisi de içerebilir. Reserved, Public ve Private olmak üzere 3 tip claim bulunur.
</p>

<ul type="circle">
    <li><b>Reserved(Ayrılmış) Claim'ler:</b> Önceden tanımlanmış claim'lerdir. Kullanılması zorunlu değildir fakat önerilir. Yararlıdır ve birlikte çalışabilen bir claim kümesinin oluşturulmasını sağlarlar. Bunlardan bazıları: iss (issuer)(yayınlayıcı), exp(expiration time)(son kullanım zamanı), sub (subject)(konu), aud (audience)(hedef kitle) ve diğerleridir.</li>
    <li><b>Public(Açık) Claim'ler:</b> İsteğe göre eklenen alanlardır. Fakat rezerve edilmiş IANA JSON Web Token Kayıtları veya URI'da tanımlanan parametreler ile kullanımın çakışmasından kaçınılmalıdır.</li>
    <li><b>Private(Gizli) Claim'ler:</b> Bilgi paylaşımı için tarafların kendi aralarında anlaştığı özel claim'lerdir.</li>
</ul>

<h3>Signature(İmza)</h3>

<p>Signature kısmının oluşturulabilmesi için base64 ile kodlanmış header, base64 ile kodlanmış payload, gizli bir kelime (secret) ve header'da tanımlanan algoritma gereklidir.</p>


<h2>JWT nasıl çalışıyor ?</h2>

<p>Kimliklendirme işlemlerinde, kullanıcı kendi kimlik bilgileriyle başarılı bir şekilde giriş yaptığında, geleneksel bir yaklaşım olan sunucu tarafında session açılıp kulanıcıya cookie dönülmesi yerine, geriye bir JWT döndürülür ve tekrar kullanılmak üzere localStorage veya cookies gibi yapılarda JWT saklanır.</p>

<p>Kullanıcı korunmuş bir kaynağa erişmek istediğinde, istemci tarafından Authorization başlığı içerisinde Bearer şeması kullanılarak JWT sunucuya iletilmelidir.</p>

<p>Buradaki stateless (durumsuz) kimliklendirme mekanizması sayesinde session’da yapılanın aksine kullanıcının durum bilgisi asla sunucunun hafızasında (RAM’inde) saklanmamış olur. Sunucudaki korunan kaynağa erişim izni Authorization başlığındaki JWT’nin geçerliliği ile kontrol edilir. Eğer geçerli ise, kullanıcının korunan kaynağa erişim izni sağlananır. JWT’ler kendi kendini betimledikleri için, bütün gerekli bilgi JWT’nin içerisindedir. Bu sayede veritabanı üzerinden kimlik doğrulama için çoklu sorgu işlemleri yapılmasının önüne geçilmiş olur.</p>

<ol type="1">
    <li>Uygulamaya giriş için kullanıcı adı ve şifre gerektiğinden; ilk adımda client bu bilgileri browser üzerinden HTTP Post ile sunucuya gönderiyor.</li>
    <li>Gönderilen kullanıcı adı ve şifre bilgileri doğrulanıyor. Bilgilerin doğru olması durumunda bir JWT üretim işlemi yapılıyor.</li>
    <li>Üretilen JWT bilgisi, isteği yapan client’a iletiliyor. Bu noktadan sonra tekrar kullanıcı adı ve şifre ile doğrulama yapılmasına gerek kalmayacaktır. Token geçerli olduğu sürece yetkilendirme işlemleri için bu token üzerinden gerçekleşecektir.</li>
    <li>Bir sonraki istek, HTTP üzerinden, JWT’yi Authorization Header bilgisine eklenerek yapılıyor.</li>
    <li>Sunucu, JWT imzasının geçerli olup olmadığını kontrol ederek JWT’nin doğrulamasını yapar.</li>
    <li>Geçerli bir JWT gönderilmişse, Authorization işlemi onaylanarak talep edilen bilgiler client’a gönderilir.</li>
</ol>
