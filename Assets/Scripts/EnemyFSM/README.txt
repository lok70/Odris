В данном ридми я объясню, как работать с FSM - системой конечных автоматов. На данный момент в FSM перенесена базовая абстрактная логика поведения противника(состояния: преследование, проверка последнего местонахождения, бездействие, атака и подозрение).

1) чтобы создать новое состояние необходимо создать класс-наследник EnemyState.cs, в файле Enemy(вначале в #region Sm states) добавить экземпляр созданного состояние, далее создать и присвоить полю экземпляр.

2) в классе состояний необходимо переопределять виртуальные методы родительского класса. В каждом состоянии придется обращаться к экземпляру Enemy.cs, в нем определены основные методы, триггеры и тд..такие как obstacklesFlag(дает true,  в случае, если между врагом и игроком препятствие)

3) думаю, вы разберетесь, глядя на мой код, старался писать понятно, а если нет, милости прошу смотреть тутор https://www.youtube.com/watch?v=RQd44qSaqww


//Немного о работе базовой логики врага

поля с окончанием Distance:

shootingDistance - дистанция атаки

chaseDistance - дистанция погони

agroDistance - дистанция подозрени

некоторые поля открыты в инспекторе(было нужно для демонстрации), отключите их - distanceFromPlayer, obstackleFlag.

//немного о TestPlayerScripts

используйте TestCharacterController - он создан для абстрактной демонстрации движений игрока(а точнее, для теста системы enemy AI).


