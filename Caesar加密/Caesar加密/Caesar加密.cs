



#region Using
// Using
// 自带
using System;
//
// 添加
using System.Collections.Generic;
using System.IO;
using System.Text;
#endregion

// 字符为单个Unicode字符集Code；字母为最小语音单位；词为字母|其余单个字符。即：1字母 = n字符，n ∈ (0, +∞]，现实中一般是1|2|3；一般称的“字符”此处称为词

// 现代汉语-普通话音节构成：[声母]韵母；韵母构成：[韵头|介音]韵腹[韵尾|尾音]

namespace Caesar加密
{
	public class 汉语拼音版Caesar加密
	{
		#region 构造器
		// 构造器
		public 汉语拼音版Caesar加密(Int32 加密子_输入 = 1)
		{
			加密子 = 加密子_输入;

			// 初始化
			全字母表.AddRange(韵母表);
			全字母表.AddRange(声母表);
			//
			最大字母长度 = 获取最大字母长度();		// ？可否化简为属性的getter，即属性中是否可以调用函数，理论上可行，因大量的操作都是既定的函数
		}
		#endregion

		#region 字段
		// 字段
		#region 常量
		// 常量|只读
		// 为支持多字符表示的声母|韵母，使用String
		// 不直接使用ASCII|Unicode码，避免出现码位不同|不连贯、码序不同的情况，同时便于定制化
		// 韵母表
		// 单字母版
		// 完整版
		// 所有“ê”由“e”+“̂”（扬抑符）构成；“ü”由“u”+“̈”（分音符）构成
		//String[] 韵腹表_小写 = {"a", "o", "e", "ê", "i", "ɹ", "r", "u", "ü"};		// 其中"ɹ"（一说为ɯ）、“r”（实为/ɻ/，即韵母r的元音相近音的一个版本）为表示字符，也可以分别写作“ih”、“rh”，其一般的IPA标识为/ɹ̩/（一说为/ɯ/）、/ɻ̩/
																												// “ê”指“ye”、“yue”的韵腹，也指独立的“欸”字4个读音的韵母；“ɹ”指“zi”、“ci”、“si”的韵母；"r"指“zhi”、“chi”、“shi”、“ri”（实际其声母、韵母非同音，但距离较近，故可视之为同一音，则“ri”可写作“r”，视之为〇声母音节，其实更符合其事实来源）的韵母
		//String[] 韵腹表_大写 = {"A", "O", "E", "Ê", "I", "ɹ", "R", "U", "Ü"};		// 其中"ɹ"没有大写对应
		//String[] 韵头表_小写 = {"i", "u", "ü"};
		//String[] 韵尾表_小写 = {"p", "t", "k", "ʔ", "m", "n", "ng", "e", "i", "r", "u", "ü"};		// 前3个为公认的汉语部分方言含有的“塞韵尾”；4th个为吴语|江浙话中简化后的“塞擦韵尾”；5th~7th既指大部分方言含有的3种“阳声韵韵尾”，也指普通话中含有的单独的鼻辅音字，如：“姆”、“呒”、“呣”、“唔”、“嗯”；“r”指儿化韵、“儿”的韵尾；余下为正常韵尾
		//
		// 通用版
		private readonly List<String> 韵母表 = new List<String> {"a", "o", "e", "ê", "i", "u", "ü"};		// 所有“ê”由“e”+“̂”（扬抑符）构成；“ü”由“u”+“̈”（分音符）构成
		//private List<String> 韵母表 = new List<String> {"a", "o", "e", "i", "u", "ü"};		// 与一般教材的韵母表保持一致，但缺少词典含有的“ê”
		//
		//
		// 声母表
		// 完整版
		//String[] 声母表_小写 = {"b", "c", "ch", "d", "f", "g", "h", "j", "k", "l", "m", "n", "ng", "p", "q", "s", "sh", "t", "x", "z", "zh"};
		// 多字母视为一体，一同连写，但“首字母大写”时仍以实际的首字母大写，便于各处表达时与“全字母大写”相区分
		//String[] 声母表_大写 = {"B", "C", "Ch", "D", "F", "G", "H", "J", "K", "L", "M", "N", "Ng", "P", "Q", "S", "Sh", "T", "X", "Z", "Zh"};
		//
		// 通用版
		private readonly List<String> 声母表 = new List<String> {"b", "p", "m", "f", "d", "t", "n", "l", "g", "k", "ng", "h", "j", "q", "x", "z", "c", "s", "zh", "ch", "sh", "r", "y", "w", "yu"};		// 观感不佳，因“yu”的加入会出现独立的声母作为音节，但更符合定义
		//private List<String> 声母表 = new List<String> {"b", "p", "m", "f", "d", "t", "n", "l", "g", "k", "h", "j", "q", "x", "z", "c", "s", "zh", "ch", "sh", "r", "y", "w"};		// 观感较好，与一般教材的声母表保持一致，但缺少“ng”、“yu”
		//
		//
		// 声调表
		// 完善版
		private List<String> 声调表 = new List<String> {"̄", "́", "̌", "̀", "·", "̂", "̈"};		// 分别是：一声-阴平（长音符，U+0304，Combining Macron）、二声-阳平（尖音符|锐音符，U+0301，Combining Acute Accent）、三声-上声（抑扬符|倒折音符，U+030C，Combining Caron）、四声-去声（重音符|抑音符|钝音符，U+0300，Combining Grave Accent）、轻声（间隔符，U+00B7，Middle Dot）、“帽子”（扬抑符|折音符，U+0302，Combining Circumflex Accent）、“两点”（分音符|曲音符，U+0308，Combining Diaeresis）
		#endregion
		#region 变量
		// 变量
		// 构造器赋值
		private Int32 最大字母长度 = default;
		private Int32 加密子 = default;
		private List<String> 全字母表 = new List<String>();
		#endregion
		#endregion

		#region 方法
		// 方法
		public StringBuilder 加密(String 明文_输入)
		{
			// ！合法性检测
			if(String.IsNullOrEmpty(明文_输入))
			{
				return default;
			}
			else
			{
				// 占位
			}

			// 预处理
			明文_输入 = 格式化明文(明文_输入);

			// 定义
			List<String> 处理明文 = 分词(明文_输入);
			StringBuilder 处理密文_输出 = new StringBuilder();

			// 处理
			foreach(String 词 in 处理明文)
			{
				处理密文_输出.Append(加密_核心(词));
			}

			return 处理密文_输出;
		}

		// ！最好参照编译器的中的分词器（Lexar）逻辑进行优化
		private List<String> 分词(String 明文_输入)
		{
			// ！合法性检测

			List<String> 明文词组_输出 = new List<String>();
			String 处理字母 = default;		// 避免无实例可判定
			Int32 索引 = (明文_输入.Length >= 2) ? Next(default) : ZeroIndexed(明文_输入.Length);		// ∈ [2, +∞) | 1
			Int32 剩余明文 = default;
			Int32 步进 = 最大字母长度;
			
			//if(明文_输入.Length >= 2)		// ∈ [2, +∞)
			//{
				// 初始值是0-Indexed下越过了1st值，故使用Next()而非OneIOneIndexed()、字面常量1
				for(索引 = Next(default); 索引 <= ZeroIndexed(明文_输入.Length); 索引 += 步进)
				{
					// 预处理
					剩余明文 = ZeroIndexed(明文_输入.Length) - 索引;
					//
					//
					if(步进 == 最大字母长度)		// 1st次 + “取空”情况
					{
						// ！最大字母长度超过2时逻辑需要加装for()保持简洁

						处理字母 = 明文_输入[Previous(索引)].ToString();
					}
					else
					{
						// 占位
					}
					//
					处理字母 += 明文_输入[索引].ToString();

					if(Is字母(处理字母))		// 仅为判断该组合的字母是否合法
														// 组合的字母
					{
						明文词组_输出.Add(处理字母);

						// 终处理
						处理字母 = default;
					}
					else		// 非组合的字母
					{
						do
						{
							// 不是组合的字母，则前一个必然是单字符字母，直接处理
							明文词组_输出.Add(处理字母[default].ToString());

							// 终处理
							// 后一个字符等待下次组合判断
							处理字母 = 处理字母.Remove(default, OneIndexed(default));		// 移除首字符
						}
						while
						(
							剩余明文 == default
							&& String.IsNullOrEmpty(处理字母) == false
						);		// 针对更大的“最大字母长度”加装的循环处理

						// 终处理
						if(String.IsNullOrEmpty(处理字母))
						{
							break;
						}
						else		// 仅此处向下次循环提供“不清〇”的“处理字母”
						{
							// 占位
						}
					}

					// 终处理
					步进 = 获取步进(处理字母, 剩余明文);
				}

				// 处理奇数个情况中的Last字符
				// 其实也可以用“处理字母.Length == default”判断
				//if(Is奇数(明文_输入))
				//{
				//	// 预处理
				//	索引 = Previous(索引);		// 回调1，若为奇数则这是last
				//	处理字母 += 明文_输入[索引].ToString();		// 2种情况：null+字符、字符+字符（2个单字符字母、1个组合的字母）

				//	if(Is字母(处理字母))		// 仅判断该双字母|多字母组合的字符是否合法
				//										// 组合的字母
				//	{
				//		明文词组_输出.Add(处理字母);
				//	}
				//	else		// 非组合的字母
				//	{
				//		// 不是组合的字符，则前一个必然是单字符字母，直接处理
				//		明文词组_输出.Add(处理字母[default].ToString());
				//		// 后一个字符是last单字符字母，直接处理
				//		明文词组_输出.Add(处理字母[^OneIndexed(default)].ToString());		// last
				//																											// ！右序索引是1-Indexed
				//	}

				//	// 终处理
				//	//处理字符 = default;
				//}
				//else		// 偶数的情况
				//{
				//	// 占位
				//}
			//}
			//else		// ∈ [0, 1]
			//{
			//	明文词组_输出.Add(处理字母);

			//	// 终处理
			//	//处理字符 = String.Empty;
			//}

			return 明文词组_输出;
		}

		// 脱离ASCII|Unicode的码序，完全依托自行创建的各字母表内容及顺序进行操作
		// 保留原大小写
		private String 加密_核心(String 明文字符_输入)
		{
			String 密文字符_输出 = default;
			Int32 索引 = default;
			String 处理字母 = default;
			Char 处理字符 = default;

			if(Is字母(明文字符_输入))
			{
				if(Is韵母(明文字符_输入.ToLowerInvariant()))		// 韵母
																						// 规划|实际只有单字符字母
				{
					索引 = 韵母表.IndexOf(明文字符_输入.ToLowerInvariant());
					处理字母 = 韵母表[获取密文字符索引(索引, 韵母表.Count)];
				}
				else if(Is声母(明文字符_输入.ToLowerInvariant()))		// 声母
				{
					索引 = 声母表.IndexOf(明文字符_输入.ToLowerInvariant());
					处理字母 = 声母表[获取密文字符索引(索引, 声母表.Count)];
				}
				else		// 理论上不存在
				{
					// 占位
				}

				// 预处理
				索引 = default;

				foreach(Char 字符 in 处理字母)		// 映射情况：
																	// 序号			明文字母字符		密文字母字符		明文字母大小写情况		密文大小写情况
																	// 1.				1						1						X									X
																	// 2.				1						1						x									x
																	// 3.				1						2						X									Xx
																	// 4.				1						2						x									xx
																	// 5.				2						1						XX								Xx
																	// 6.				2						1						Xx									Xx
																	// 7.				2						1						xX									xx
																	// 8.				2						1						xx									xx
																	// 9.				2						2						XX								Xx
																	// 10.			2						2						Xx									Xx
																	// 11.			2						2						xX									xx
																	// 12.			2						2						xx									xx
				{
					if
					(
						索引 <= ZeroIndexed(明文字符_输入.Length)		// 防止1 → 2时大小写参照用的明文字母索引越界
						&& Char.IsUpper(明文字符_输入[索引])
					)		// [索引]字符是大写字母
					{
						密文字符_输出 += 字符.ToString().ToUpperInvariant();		// [索引]字符大写
					}
					else
					{
						密文字符_输出 += 字符.ToString();
					}

					// 终处理
					索引++;
				}
			}
			else		// 其他符号：原样输出
			{
				密文字符_输出 = 明文字符_输入;
			}

			return 密文字符_输出;
		}

		// 专用独立字符→La字母+组合符号
		// 思来想去，的确无法进一步简化|优化，因这些都是因国家|语言标准而定，而大陆并未提供Unicode表中完善的汉语拼音用符，故仅能“依常贯之”，且各符分散各处
		public String 格式化明文(String 原文_输入)
		{
			String 明文_输出 = 原文_输入;

			// 字母
			// 韵母“ê”
			明文_输出 = 明文_输出.Replace("ê", "ê");
			明文_输出 = 明文_输出.Replace("ê".ToUpperInvariant(), "ê".ToUpperInvariant());
			// 韵母“ü”
			明文_输出 = 明文_输出.Replace("ü", "ü");
			明文_输出 = 明文_输出.Replace("ü".ToUpperInvariant(), "ü".ToUpperInvariant());

			// 声调
			// 一声-阴平
			// 韵母“a”
			明文_输出 = 明文_输出.Replace("ā", "ā");
			明文_输出 = 明文_输出.Replace("ā".ToUpperInvariant(), "ā".ToUpperInvariant());
			// 韵母“o”
			明文_输出 = 明文_输出.Replace("ō", "ō");
			明文_输出 = 明文_输出.Replace("ō".ToUpperInvariant(), "ō".ToUpperInvariant());
			// 韵母“e”
			明文_输出 = 明文_输出.Replace("ē", "ē");
			明文_输出 = 明文_输出.Replace("ē".ToUpperInvariant(), "ē".ToUpperInvariant());
			// 韵母“ê”
			// 中方未为“ê”制定独立的带声调符号，且现有字符中无“ê”+“̄”（长音符）的组合，故无所可替
			//明文_输出 = 明文_输出.Replace("", "ê̄");
			//明文_输出 = 明文_输出.Replace("".ToUpperInvariant(), "ê̄".ToUpperInvariant());
			// 韵母“i”
			明文_输出 = 明文_输出.Replace("ī", "ī");
			明文_输出 = 明文_输出.Replace("ī".ToUpperInvariant(), "ī".ToUpperInvariant());
			// 韵母“u”
			明文_输出 = 明文_输出.Replace("ū", "ū");
			明文_输出 = 明文_输出.Replace("ū".ToUpperInvariant(), "ū".ToUpperInvariant());
			// 韵母“ü”
			明文_输出 = 明文_输出.Replace("ǖ", "ǖ");
			明文_输出 = 明文_输出.Replace("ǖ".ToUpperInvariant(), "ǖ".ToUpperInvariant());
			//
			// 二声-阳平
			// 韵母“a”
			明文_输出 = 明文_输出.Replace("á", "á");
			明文_输出 = 明文_输出.Replace("á".ToUpperInvariant(), "á".ToUpperInvariant());
			// 韵母“o”
			明文_输出 = 明文_输出.Replace("ó", "ó");
			明文_输出 = 明文_输出.Replace("ó".ToUpperInvariant(), "ó".ToUpperInvariant());
			// 韵母“e”
			明文_输出 = 明文_输出.Replace("é", "é");
			明文_输出 = 明文_输出.Replace("é".ToUpperInvariant(), "é".ToUpperInvariant());
			// 韵母“ê”
			明文_输出 = 明文_输出.Replace("ế", "ê̄");
			明文_输出 = 明文_输出.Replace("ế".ToUpperInvariant(), "ê̄".ToUpperInvariant());
			// 韵母“i”
			明文_输出 = 明文_输出.Replace("í", "í");
			明文_输出 = 明文_输出.Replace("í".ToUpperInvariant(), "í".ToUpperInvariant());
			// 韵母“u”
			明文_输出 = 明文_输出.Replace("ú", "ú");
			明文_输出 = 明文_输出.Replace("ú".ToUpperInvariant(), "ú".ToUpperInvariant());
			// 韵母“ü”
			明文_输出 = 明文_输出.Replace("ǘ", "ǘ");
			明文_输出 = 明文_输出.Replace("ǘ".ToUpperInvariant(), "ǘ".ToUpperInvariant());
			//
			// 三声-上声
			// 韵母“a”
			明文_输出 = 明文_输出.Replace("ǎ", "ǎ");
			明文_输出 = 明文_输出.Replace("ǎ".ToUpperInvariant(), "ǎ".ToUpperInvariant());
			// 韵母“o”
			明文_输出 = 明文_输出.Replace("ǒ", "ǒ");
			明文_输出 = 明文_输出.Replace("ǒ".ToUpperInvariant(), "ǒ".ToUpperInvariant());
			// 韵母“e”
			明文_输出 = 明文_输出.Replace("ě", "ě");
			明文_输出 = 明文_输出.Replace("ě".ToUpperInvariant(), "ě".ToUpperInvariant());
			// 韵母“ê”
			// 中方未为“ê”制定独立的带声调符号，且现有字符中无“ê”+“̌”（抑扬符）的组合，故无所可替
			//明文_输出 = 明文_输出.Replace("", "ê̌");
			//明文_输出 = 明文_输出.Replace("".ToUpperInvariant(), "ê̌".ToUpperInvariant());
			// 韵母“i”
			明文_输出 = 明文_输出.Replace("ǐ", "ǐ");
			明文_输出 = 明文_输出.Replace("ǐ".ToUpperInvariant(), "ǐ".ToUpperInvariant());
			// 韵母“u”
			明文_输出 = 明文_输出.Replace("ǔ", "ǔ");
			明文_输出 = 明文_输出.Replace("ǔ".ToUpperInvariant(), "ǔ".ToUpperInvariant());
			// 韵母“ü”
			明文_输出 = 明文_输出.Replace("ǚ", "ǚ");
			明文_输出 = 明文_输出.Replace("ǚ".ToUpperInvariant(), "ǚ".ToUpperInvariant());
			//
			// 四声-去声
			// 韵母“a”
			明文_输出 = 明文_输出.Replace("à", "à");
			明文_输出 = 明文_输出.Replace("à".ToUpperInvariant(), "à".ToUpperInvariant());
			// 韵母“o”
			明文_输出 = 明文_输出.Replace("ò", "ò");
			明文_输出 = 明文_输出.Replace("ò".ToUpperInvariant(), "ò".ToUpperInvariant());
			// 韵母“e”
			明文_输出 = 明文_输出.Replace("è", "è");
			明文_输出 = 明文_输出.Replace("è".ToUpperInvariant(), "è".ToUpperInvariant());
			// 韵母“ê”
			明文_输出 = 明文_输出.Replace("ề", "ề");
			明文_输出 = 明文_输出.Replace("ề".ToUpperInvariant(), "ề".ToUpperInvariant());
			// 韵母“i”
			明文_输出 = 明文_输出.Replace("ì", "ì");
			明文_输出 = 明文_输出.Replace("ì".ToUpperInvariant(), "ì".ToUpperInvariant());
			// 韵母“u”
			明文_输出 = 明文_输出.Replace("ù", "ù");
			明文_输出 = 明文_输出.Replace("ù".ToUpperInvariant(), "ù".ToUpperInvariant());
			// 韵母“ü”
			明文_输出 = 明文_输出.Replace("ǜ", "ǜ");
			明文_输出 = 明文_输出.Replace("ǜ".ToUpperInvariant(), "ǜ".ToUpperInvariant());

			return 明文_输出;
		}
		#region 工具
		// 工具
		private Boolean Is字母(String 字符_输入) => 全字母表.Contains(字符_输入.ToLowerInvariant());
		private Boolean Is韵母(String 字符_输入) => 韵母表.Contains(字符_输入.ToLowerInvariant());
		private Boolean Is声母(String 字符_输入) => 声母表.Contains(字符_输入.ToLowerInvariant());
		//
		private Boolean Is奇数(String 明文_输入) => 明文_输入.Length % 2 != default;		// 相较于 == 1，!= 0更能说明奇数的性质：有余数，而余数是几没有这个“牢靠”
		//
		private Int32 OneIndexed(Int32 ZeroIndexed_输入) => Next(ZeroIndexed_输入);
		private Int32 ZeroIndexed(Int32 OneIndexed_输入) => Previous(OneIndexed_输入);
		//
		private Int32 获取密文字符索引(Int32 明文字符_输入, Int32 对应全集长度_输入) => (明文字符_输入 + 加密子) % 对应全集长度_输入;
		//
		private Int32 Next(Int32 源数_输入) => 源数_输入 + 1;
		private Int32 Previous(Int32 OneIndexed_输入)
		{
			if(OneIndexed_输入 == default)
			{
				return default;
			}
			else
			{
				return OneIndexed_输入 - 1;
			}
		}
		//
		public void 写入文件(StringBuilder 内容_输入, String 路径_输入)
		{
			// ！未做合法性检测

			StreamWriter 写入器 = new StreamWriter(new FileStream(路径_输入, FileMode.Create), Encoding.UTF8);		// ！ASCII节约地方；UTF-8通用；UTF-32|UCS-4直接

			写入器.Write(内容_输入);

			// ？不需要写入Flush()

			写入器.Close();

			写入器.Dispose();
		}
		public String 读取文件(String 路径_输入)
		{
			// ！未做合法性检测
			StreamReader 读取器 = new StreamReader(路径_输入);
			String 内容_输出 = default;

			内容_输出 = 读取器.ReadToEnd();

			// ？不需要写入Flush()

			// 终处理
			读取器.Close();
			读取器.Dispose();

			return 内容_输出;
		}
		//
		private Int32 获取步进(String 决定值_输入, Int32 余值_输入)
		{
			Int32 步进_输出 = default;

			if(String.IsNullOrEmpty(决定值_输入))
			{
				if(余值_输入 >= 最大字母长度)
				{
					步进_输出 = 最大字母长度;
				}
				else
				{
					步进_输出 = 余值_输入;
				}
			}
			else
			{
				步进_输出 = 最大字母长度 - 决定值_输入.Length;
			}

			return 步进_输出;
		}
		//
		private Int32 获取最大字母长度()
		{
			Int32 长度_输出 = default;

			foreach(String 字母 in 全字母表)
			{
				if(长度_输出 < 字母.Length)		// ？写法可以化简，类似is|as
				{
					长度_输出 = 字母.Length;
				}
				else
				{
					// 占位
				}
			}

			return 长度_输出;
		}
		#endregion
		#endregion
	}
}