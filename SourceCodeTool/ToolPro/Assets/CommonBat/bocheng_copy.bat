
:: 源文件
set soureceDir=F:\ProjectqiPai\qipai5\bl_app_client\client\Assets\GameMain\Games\DZMJ\*

set dirctionDir=F:\ProjectqiPai\bocheng_client\bl_app_client\client\Assets\GameMain\Games\DZMJ
rd /s /q %dirctionDir%

md %dirctionDir%


XCOPY %soureceDir% %dirctionDir% /s /e

