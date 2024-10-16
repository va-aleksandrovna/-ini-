Создать инструмент для обработки конфигурационного INI файла. 
Описать и реализовать необходимые классы, которые позволят производить обработку конфигурационного файла, который представляет собой текстовый файл, разделенный на СЕКЦИИ, которые содержат пары ИМЯ, ЗНАЧЕНИЕ. 
Пример файла: 
; Example of INI file 
[COMMON] 
StatisterTimeMs = 5000 
LogNCMD = 1 ; Logging ncmd proto 
LogXML = 0 ; Logging XML proto 
DiskCachePath = /sata/panorama ; Path for file cache 
OpenMPThreadsCount = 2 
[ADC_DEV] 
BufferLenSeconds = 0.65 ; Buffer length for ADC data in GPU memory, seconds. 
SampleRate = 120000000.0 ; Sample rate of ADC. 
Driver = libusb ; cypress / libusb / random / fileIQS 
[NCMD] 
EnableChannelControl = 1 ; Use or not CHG / CHGEXT commands 
SampleRate = 900000.0 ; ANOTHER Sample Rate. 
TidPacketVersionForTidControlCommand = 2 
; TidPacket versions 
; 0 - no packets 
; 1 - header: data size, tid 
; 2 - header: data size, tid, timestamp 
[LEGACY_XML] 
ListenTcpPort = 1976 
[DEBUG] 
PlentySockMaxQSize = 126 
DBAddressIP = 127.0.0.1 
Все имена параметров и секций – это строки без пробелов, состоящие из символов латинского алфавита и цифр. 
Секции – заключены в квадратные скобки, без пробелов. 
Значения параметров отделены от имен параметров знаком = (равенство). Для простоты можно считать, что слева и справа от знака равенства находится как минимум один пробел (но на самом деле, это не так). 
Значения параметров могут быть одним из типов: 
целочисленным, 
вещественным, 
строковым: без пробелов, но в отличие от имени параметра может содержать также символ «точка». 
Файл может содержать комментарии. Комментарием считается всё, что находится после знака «точка с запятой». Комментарии, как и сам знак «точка с запятой» должны быть проигнорированы. 

Доступ к парсеру осуществляется через класс IniParser, который должен: 
 Принимать любым выбранным вами образом на вход имя файла в виде строки. Либо как параметр конструктора, либо отдельным методом. 
 Обработку файла и сохранения всей информации во внутренней памяти. 
 Возвращать значение параметра по названию секции и имени параметра. 
Например,
«Получить вещественное значение параметра SampleRate из секции ADC_DEV». 
«Получить вещественное значение параметра SampleRate из секции NCMD». 
«Получить строковое значение параметра DBAddressIP из секции DEBUG». 

• Так как значение параметра может быть одним из трех типов, то предполагается наличие трех разных методов, либо использование шаблонных функций. 
• Сигнализировать об ошибках путем генерации исключений: 
• Ошибка файловой подсистемы (например, если файл не найден). 
• Ошибка формата файла (если файл имеет неверный формат). 
• Неверный тип параметра (ошибка при приведении типа). 
• Заданной пары СЕКЦИЯ ПАРАМЕТР нет в конфигурационном файле. 
• При уничтожении экземпляра класса правильным образом уничтожать все созданные объекты. 

К заданию приложены несколько примеров входных файлов. Вы можете использовать их для последовательного тестирования программы. 
Ниже приводится рекомендуемый план работы над заданием. 
С учетом вашего опыта и квалификации, вы можете выбрать другую последовательность решения задачи. 

• Написать процедуру, которая открывает файл, считывает его построчно и выводит эти строки на экран. Проверить. 
• Найти в каждой считанной строке секцию или имя-и-параметр. Для начала использовать файл без секций, комментариев, лишних строк. Затем постепенно добавлять: лишние пустые строки, комментарии, добавлять пробелы после имен параметров, после значений и т.д. 

Контролировать каждый шаг, выводя на экран необходимую отладочную информацию 
• Изменить процедуру таким образом, чтобы можно было сопоставить имя параметра секции. 
• Прогнать выданные вам тестовые файлы через процедуру. 
• Радоваться, что сложная часть позади. 

Часть 2 
• Описать (без реализации) интерфейс класса IniParser. 
• Описать с реализацией необходимый набор исключений. 
• Продумать внутренние структуры данных для хранения информации из конфигурационного файла. 
Совет: храните ЗНАЧЕНИЕ как строку; делайте попытку перевести её в int или double только при обращении. При неудачной попытке генерируйте исключение «неверный тип параметра». 
• Описать вспомогательные классы или структуры. 
• Написать отладочную функцию класса, которая бы полностью обходила внутренние структуры и выводила их на экран. Проверить, что она работает в крайних случаях (когда данных нет). Затем вручную временно заполнить эти структуры и проверить вывод. Также вы таким образом отладите функции добавления данных (и обнаружите их необходимость). 

Часть 3 
• Аккуратно переместить написанную в части 1 процедуру в класс. 
• Изменить её таким образом, чтобы она заполняла ваши структуры данных, а не просто выводила информацию на экран. 
