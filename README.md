[//]:# (Microsoft YaHei UI)

·Caesar加密  
\-该版本代码为为汉语拼音加密的实现  
\-对古汉语拟音、方言等有一定的预留扩展性  
\-以国际音标（IPA）为基准进行字符的选用  
\-使用未修改|还原的汉语拼音，如：ao→au、ui→uei、u|yu→ü、ng→ŋ等  
\-只对明文中的字母进行加密，不对含声调字母、其他符号进行加密；区分字母的大小写，即各为一体加密，不混淆  
//组合字母的Combining Character、Vowel Sign、声调符号等不做处理  
\-针对汉语拼音的连读|拼写规则，为使密文能够进行一定的朗读，视部分多字符声母作为单个字母  
\-此加密算法为对称加密，仅推荐作娱乐之用  
\-对[Tur测试](http://www.moserware.com/2008/02/does-your-code-pass-turkey-test.html)、非常规字符集、[Unicode常见坑点](https://zhuanlan.zhihu.com/p/53714077)有一定的注意及规避  
\-区分数字的使用方式：区分索引递增|递减用运算、正常运算；〇索引化数字、一索引化数字  

·样例：  
\-原文：[《特稿：70年风雨兼程　中俄关系何以成就“三个最高”》](https://www.xuexi.cn/lgpage/detail/index.html?id=12361664432301269175)（节选）  
\-明文：  
### [Tegao：70 Nian Fengyujiancheng，Zhong-E Guanxi He Yi Chengjiu “San Ge Zuigao”](https://github.com/MetallicPickaxe/Caesar-Cipher/blob/master/Read%20Me%E7%94%A8%E6%A0%B7%E4%BE%8B/%E5%8A%A0%E5%AF%86-Caesar-%E6%B1%89%E8%AF%AD%E6%8B%BC%E9%9F%B3%E7%89%88-%E6%98%8E%E6%96%87.txt)
##### 2019-06-02　　Laiyuan：Xinhuawang
#### Xinhuashe Beijing 6 Yue 2 Ri dian　Tegao：70 Nian Fengyujiancheng，Zhong-E Guanxi He Yi Chengjiu “San Ge Zuigao”
        Guojia Zhuxi Xi Jinping jijiang dui Eluosi jinxing guoshi fangwen，bing chuxi Shengbidebao Guoji Jingji Luntan。Jinnian shi Zhong-E jianjiao 70 zhounian，cifang bijiang tuidong Zhong-E guanxi zai xinde lishi qidian shang kaiqi genggao shuiping、 gengda fazhan de xinshidai。
        70 nian lai，Zhong-E guanxi fengyujiancheng、diliqianxing，chengwei huxin chengdu zuigao、xiezuo-shuiping zuigao、zhanlüe-jiazhi zuigao de yidui daguo guanxi，bujin gei liangguo minzhong dailai juda fuzhi，yewei shijie heping-wending zhuru zhengnengliang。Zai dangjin shijie bainianweiyou zhi dabianju zhong，Zhong-E mianlin gongtong de renwu he tiaozhan，youhao-hezuo-zhi-lu jiang yuezouyuekuan。
\-密文：  
### [Nêkoe：70 Luol Dêhbquolshêh，Cheh-Ê Küolzu Jê Wu Shêhquü “Zhol Kê Cüukoe”](https://github.com/MetallicPickaxe/Caesar-Cipher/blob/master/Read%20Me%E7%94%A8%E6%A0%B7%E4%BE%8B/%E5%8A%A0%E5%AF%86-Caesar-%E6%B1%89%E8%AF%AD%E6%8B%BC%E9%9F%B3%E7%89%88-%E5%AF%86%E6%96%87.txt)
##### 2019-06-02　　Goubol：Zuljüoyuoh
#### Zuljüorê Pêuquh 6 Bê 2 Yu tuol　Nêkoe：70 Luol Dêhbquolshêh，Cheh-Ê Küolzu Jê Wu Shêhquü “Zhol Kê Cüukoe”
        Küequo Chüzu Zu Qulmuh ququoh tüu Êgüezhu qulzuh küeru dohyuêl，puh shüzu Rêhputêpoe Küequ Quhqu Gülnol。Qulluol ru Cheh-Ê quolquoe 70 cheüluol，sudoh puquoh nüuteh Cheh-Ê küolzu cou zultê guru xutuol roh ngouxu kêhkoe rüumuh、 kêhto dochol tê zulrutou。
        70 luol gou，Cheh-Ê küolzu dêhbquolshêh、tuguxuolzuh，shêhyuêu jüzul shêhtü cüukoe、zuêcüe-rüumuh cüukoe、cholgüê-quochu cüukoe tê wutüu toküe küolzu，püqul kêu guohküe fulcheh tougou qüto düchu，wêyuêu ruquê jêmuh-yuêltuh chüyü chêhlêhguoh。Cou tohqul ruquê pouluolyuêuweü chu topuolqü cheh，Cheh-Ê fuolgul kehneh tê yêlyuü jê nuoechol，weüjoe-jêcüe-chu-gü quoh bêceübêngüol。