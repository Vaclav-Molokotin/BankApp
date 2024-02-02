-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Фев 02 2024 г., 18:44
-- Версия сервера: 8.0.31
-- Версия PHP: 8.0.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `bankdb`
--

-- --------------------------------------------------------

--
-- Структура таблицы `bill`
--

DROP TABLE IF EXISTS `bill`;
CREATE TABLE IF NOT EXISTS `bill` (
  `Number` char(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Номер платёжного счёта',
  `Balance` float UNSIGNED NOT NULL COMMENT 'Баланс платёжного счёта',
  `OwnerID` int UNSIGNED NOT NULL COMMENT 'Владелец',
  `CardNumber` char(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT 'Привязанная карта',
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT 'Название счёта',
  `StatusID` int NOT NULL COMMENT 'Статус счёта',
  PRIMARY KEY (`Number`),
  KEY `bill_OwnerID_FK` (`OwnerID`),
  KEY `CardNumber` (`CardNumber`),
  KEY `StatusID` (`StatusID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='Таблица платёжного счёта';

--
-- Дамп данных таблицы `bill`
--

INSERT INTO `bill` (`Number`, `Balance`, `OwnerID`, `CardNumber`, `Name`, `StatusID`) VALUES
('08000388372133625204', 0, 18, NULL, NULL, 3),
('26024237815656404106', 4101, 17, '6646133656370511', NULL, 2),
('32411784174580863134', 0, 18, NULL, NULL, 3),
('47726413266743111841', 1.42001, 3, '0731264144847107', NULL, 2),
('51175573861304508410', 1925.45, 3, NULL, NULL, 3),
('73151083351717356481', 0, 3, NULL, NULL, 2),
('73460551667445640885', 0, 17, NULL, NULL, 2),
('83280100721302123130', 1131.35, 3, '2304177108288457', NULL, 2);

-- --------------------------------------------------------

--
-- Структура таблицы `billstatus`
--

DROP TABLE IF EXISTS `billstatus`;
CREATE TABLE IF NOT EXISTS `billstatus` (
  `ID` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `Name` varchar(100) COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Название статуса',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `BillStatis_Name` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Дамп данных таблицы `billstatus`
--

INSERT INTO `billstatus` (`ID`, `Name`) VALUES
(2, 'Активен'),
(3, 'Закрыт'),
(1, 'Заморожен');

-- --------------------------------------------------------

--
-- Структура таблицы `card`
--

DROP TABLE IF EXISTS `card`;
CREATE TABLE IF NOT EXISTS `card` (
  `Number` char(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Номер карты',
  `CVC` char(3) COLLATE utf8mb4_general_ci NOT NULL COMMENT 'CVC-Код',
  `DateTo` date NOT NULL COMMENT 'Дата окончания действия карты',
  `DateFrom` date NOT NULL COMMENT 'Дата начала действия карты',
  `StatusID` int NOT NULL COMMENT 'Статус карты',
  PRIMARY KEY (`Number`),
  KEY `StatusID` (`StatusID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='Таблица дебитовых карт';

--
-- Дамп данных таблицы `card`
--

INSERT INTO `card` (`Number`, `CVC`, `DateTo`, `DateFrom`, `StatusID`) VALUES
('0323161350852826', '321', '2025-01-10', '2024-01-10', 3),
('0731264144847107', '202', '2025-01-30', '2024-01-30', 1),
('1644658461877238', '586', '2025-01-13', '2024-01-13', 3),
('2304177108288457', '171', '2025-01-11', '2024-01-11', 1),
('3871730778157768', '788', '2025-01-10', '2024-01-10', 3),
('4827187351843667', '765', '2025-01-10', '2024-01-10', 3),
('6646133656370511', '162', '2025-01-15', '2024-01-15', 3),
('7455032188584186', '39', '2025-01-10', '2024-01-10', 3);

-- --------------------------------------------------------

--
-- Структура таблицы `cardstatus`
--

DROP TABLE IF EXISTS `cardstatus`;
CREATE TABLE IF NOT EXISTS `cardstatus` (
  `ID` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `Name` varchar(100) COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Название статуса',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `CardStatus` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='Статус карты';

--
-- Дамп данных таблицы `cardstatus`
--

INSERT INTO `cardstatus` (`ID`, `Name`) VALUES
(1, 'Активна'),
(3, 'Заблокирована'),
(2, 'Заморожена');

-- --------------------------------------------------------

--
-- Структура таблицы `role`
--

DROP TABLE IF EXISTS `role`;
CREATE TABLE IF NOT EXISTS `role` (
  `ID` int NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор роли',
  `Name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Наименование роли',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Role_Name_UK` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='Таблица-справочник ролей пользователей';

--
-- Дамп данных таблицы `role`
--

INSERT INTO `role` (`ID`, `Name`) VALUES
(1, 'Администратор'),
(3, 'Клиент'),
(2, 'Менеджер');

-- --------------------------------------------------------

--
-- Структура таблицы `transaction`
--

DROP TABLE IF EXISTS `transaction`;
CREATE TABLE IF NOT EXISTS `transaction` (
  `ID` int UNSIGNED NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор транзакции',
  `TypeID` int NOT NULL COMMENT 'Тип транзакции',
  `BillToNumber` char(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Счёт получателя',
  `BillFromNumber` char(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Счёт отправителя',
  `StatusID` int DEFAULT NULL COMMENT 'Статус',
  `Amount` float UNSIGNED NOT NULL COMMENT 'Сумма перевода',
  `Date` date NOT NULL COMMENT 'Дата перевода',
  `Sender` varchar(75) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Логин отправителя',
  PRIMARY KEY (`ID`),
  KEY `TransactionTypeID` (`TypeID`),
  KEY `StatusID` (`StatusID`),
  KEY `BillToNumber` (`BillToNumber`),
  KEY `BillFromNumber` (`BillFromNumber`)
) ENGINE=InnoDB AUTO_INCREMENT=66 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Дамп данных таблицы `transaction`
--

INSERT INTO `transaction` (`ID`, `TypeID`, `BillToNumber`, `BillFromNumber`, `StatusID`, `Amount`, `Date`, `Sender`) VALUES
(32, 1, '47726413266743111841', '47726413266743111841', NULL, 12, '0000-00-00', 'Anto_Pop'),
(33, 1, '83280100721302123130', '47726413266743111841', NULL, 13, '0000-00-00', 'Anto_Pop'),
(40, 1, '51175573861304508410', '47726413266743111841', 1, 23, '0000-00-00', 'Anto_Pop'),
(50, 2, '47726413266743111841', '47726413266743111841', 1, 12, '0000-00-00', 'Anto_Pop'),
(51, 2, '51175573861304508410', '47726413266743111841', 1, 12, '0000-00-00', 'Anto_Pop'),
(52, 2, '51175573861304508410', '47726413266743111841', 1, 12, '0000-00-00', 'Anto_Pop'),
(53, 2, '51175573861304508410', '47726413266743111841', 1, 12, '0000-00-00', 'Anto_Pop'),
(54, 2, '51175573861304508410', '47726413266743111841', 1, 12, '0000-00-00', 'Anto_Pop'),
(55, 2, '51175573861304508410', '47726413266743111841', 1, 12, '0000-00-00', 'Anto_Pop'),
(56, 2, '51175573861304508410', '47726413266743111841', 1, 12, '0000-00-00', 'Anto_Pop'),
(57, 2, '47726413266743111841', '51175573861304508410', 1, 12, '0000-00-00', 'Anto_Pop'),
(58, 2, '51175573861304508410', '47726413266743111841', 1, 12, '0000-00-00', 'Anto_Pop'),
(59, 2, '51175573861304508410', '47726413266743111841', 1, 234, '0000-00-00', 'Anto_Pop'),
(60, 2, '47726413266743111841', '51175573861304508410', 1, 123, '0000-00-00', 'Anto_Pop'),
(61, 2, '51175573861304508410', '47726413266743111841', 1, 98, '0000-00-00', 'Anto_Pop'),
(62, 2, '51175573861304508410', '47726413266743111841', 1, 3, '0000-00-00', 'Anto_Pop'),
(63, 2, '51175573861304508410', '47726413266743111841', 1, 34, '0000-00-00', 'Anto_Pop'),
(64, 2, '51175573861304508410', '83280100721302123130', 1, 3, '0000-00-00', 'Anto_Pop'),
(65, 1, '83280100721302123130', '26024237815656404106', 1, 134, '2024-01-15', 'JoBiden');

--
-- Триггеры `transaction`
--
DROP TRIGGER IF EXISTS `transaction_trigger_after`;
DELIMITER $$
CREATE TRIGGER `transaction_trigger_after` AFTER INSERT ON `transaction` FOR EACH ROW BEGIN
UPDATE `bill` SET `Balance` = `Balance` - NEW.`Amount` WHERE `Number` = NEW.`billFromNumber`;
UPDATE `bill` SET `Balance` = `Balance` + NEW.`Amount` WHERE `Number` = NEW.`billToNumber`;
END
$$
DELIMITER ;
DROP TRIGGER IF EXISTS `transaction_trigger_before_insert`;
DELIMITER $$
CREATE TRIGGER `transaction_trigger_before_insert` BEFORE INSERT ON `transaction` FOR EACH ROW SET NEW.`StatusID` = 1, NEW.`Date` = NOW()
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Структура таблицы `transactionstatus`
--

DROP TABLE IF EXISTS `transactionstatus`;
CREATE TABLE IF NOT EXISTS `transactionstatus` (
  `ID` int NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор статуса',
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `TransactionStatus_Name_UK` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='Таблица-справочник статусов транзакций';

--
-- Дамп данных таблицы `transactionstatus`
--

INSERT INTO `transactionstatus` (`ID`, `Name`) VALUES
(3, 'Доставлен'),
(2, 'Отменён'),
(1, 'Отправлен');

-- --------------------------------------------------------

--
-- Структура таблицы `transactiontype`
--

DROP TABLE IF EXISTS `transactiontype`;
CREATE TABLE IF NOT EXISTS `transactiontype` (
  `ID` int NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор типа',
  `Name` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Наименование типа',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `TransactionType_Name_UK` (`Name`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='Таблица-справочник типов транзакций';

--
-- Дамп данных таблицы `transactiontype`
--

INSERT INTO `transactiontype` (`ID`, `Name`) VALUES
(1, 'Перевод'),
(2, 'ПереводМеждуСвоими'),
(3, 'Пополнение'),
(4, 'Снятие');

-- --------------------------------------------------------

--
-- Структура таблицы `user`
--

DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `ID` int UNSIGNED NOT NULL AUTO_INCREMENT COMMENT 'Идентификатор пользователя',
  `FName` varchar(75) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Имя пользователя',
  `LName` varchar(75) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Фамилия пользователя',
  `MName` varchar(75) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Отчество пользователя',
  `Login` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Логин',
  `Password` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Пароль, хешированный функцией MD5',
  `Phone` char(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL COMMENT 'Телефон пользователя',
  `RoleID` int NOT NULL COMMENT 'Роль пользователя',
  `StatusID` int NOT NULL COMMENT 'Статус пользователя',
  `CreationDate` datetime NOT NULL COMMENT 'Дата и время создания аккаунта',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `User_Login_UK` (`Login`),
  UNIQUE KEY `User_Phone_UK` (`Phone`),
  KEY `User_RoleID_FK` (`RoleID`),
  KEY `StatusID` (`StatusID`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='Таблица пользователей';

--
-- Дамп данных таблицы `user`
--

INSERT INTO `user` (`ID`, `FName`, `LName`, `MName`, `Login`, `Password`, `Phone`, `RoleID`, `StatusID`, `CreationDate`) VALUES
(1, 'Евгений', 'Прохоров', 'Павлович', 'Ev_Pav', '26fe0cdfe99bfa306e31733c4e2b17dc', NULL, 3, 1, '0000-00-00 00:00:00'),
(2, 'Екатерина', 'Лебедева', 'Никитична', 'Eka_Leb', '26fe0cdfe99bfa306e31733c4e2b17dc', '4294967295', 3, 1, '0000-00-00 00:00:00'),
(3, 'Антонио', 'Попов', 'Робертович', 'Anto_Pop', '202cb962ac59075b964b07152d234b70', '+7-935-245-28-34', 3, 1, '0000-00-00 00:00:00'),
(16, 'Владислав', 'Пингвинов', 'Данилович', 'vladOS', '202cb962ac59075b964b07152d234b70', '+9-234-815-34-91', 3, 1, '2024-01-10 23:13:56'),
(17, 'Максим', 'Кабанов', 'Львович', 'JoBiden', '202cb962ac59075b964b07152d234b70', '+7-235-234-23-23', 3, 1, '2024-01-15 01:13:31'),
(18, 'Геннадий', 'Бомбоненко', 'Романович', 'gench', '202cb962ac59075b964b07152d234b70', '+7-938-723-61-83', 3, 1, '2024-01-30 23:25:25');

--
-- Триггеры `user`
--
DROP TRIGGER IF EXISTS `user_trigger`;
DELIMITER $$
CREATE TRIGGER `user_trigger` BEFORE INSERT ON `user` FOR EACH ROW SET NEW.CreationDate = NOW()
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Структура таблицы `userstatus`
--

DROP TABLE IF EXISTS `userstatus`;
CREATE TABLE IF NOT EXISTS `userstatus` (
  `ID` int NOT NULL,
  `Name` varchar(75) COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `UserStatus_Name` (`Name`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='Статус пользователя';

--
-- Дамп данных таблицы `userstatus`
--

INSERT INTO `userstatus` (`ID`, `Name`) VALUES
(1, 'Активен'),
(2, 'Заблокирован'),
(3, 'Удалён');

-- --------------------------------------------------------

--
-- Дублирующая структура для представления `v_bill`
-- (См. Ниже фактическое представление)
--
DROP VIEW IF EXISTS `v_bill`;
CREATE TABLE IF NOT EXISTS `v_bill` (
`Number` char(20)
,`Balance` float unsigned
,`Owner` varchar(50)
,`CardNumber` char(16)
,`Name` varchar(50)
,`StatusID` int
);

-- --------------------------------------------------------

--
-- Дублирующая структура для представления `v_transaction_transfer`
-- (См. Ниже фактическое представление)
--
DROP VIEW IF EXISTS `v_transaction_transfer`;
CREATE TABLE IF NOT EXISTS `v_transaction_transfer` (
`Type` varchar(50)
,`BillFromNumber` char(20)
,`BillToNumber` char(20)
,`Recipient` varchar(227)
,`Amount` float unsigned
,`ID` int unsigned
,`Date` date
,`Status` varchar(50)
,`OwnerID` int unsigned
);

-- --------------------------------------------------------

--
-- Дублирующая структура для представления `v_user`
-- (См. Ниже фактическое представление)
--
DROP VIEW IF EXISTS `v_user`;
CREATE TABLE IF NOT EXISTS `v_user` (
`ID` int unsigned
,`FName` varchar(75)
,`LName` varchar(75)
,`MName` varchar(75)
,`Login` varchar(50)
,`Password` varchar(32)
,`Phone` char(16)
,`CreationDate` datetime
,`StatusName` varchar(75)
,`Role` varchar(100)
);

-- --------------------------------------------------------

--
-- Структура для представления `v_bill`
--
DROP TABLE IF EXISTS `v_bill`;

DROP VIEW IF EXISTS `v_bill`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_bill`  AS SELECT `b`.`Number` AS `Number`, `b`.`Balance` AS `Balance`, `u`.`Login` AS `Owner`, `b`.`CardNumber` AS `CardNumber`, `b`.`Name` AS `Name`, `b`.`StatusID` AS `StatusID` FROM (`bill` `b` join `user` `u` on((`b`.`OwnerID` = `u`.`ID`)))  ;

-- --------------------------------------------------------

--
-- Структура для представления `v_transaction_transfer`
--
DROP TABLE IF EXISTS `v_transaction_transfer`;

DROP VIEW IF EXISTS `v_transaction_transfer`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_transaction_transfer`  AS SELECT `tt`.`Name` AS `Type`, `t`.`BillFromNumber` AS `BillFromNumber`, `t`.`BillToNumber` AS `BillToNumber`, concat(`u`.`LName`,' ',`u`.`FName`,' ',`u`.`MName`) AS `Recipient`, `t`.`Amount` AS `Amount`, `t`.`ID` AS `ID`, `t`.`Date` AS `Date`, `ts`.`Name` AS `Status`, `uu`.`ID` AS `OwnerID` FROM ((((((`transaction` `t` join `transactiontype` `tt` on((`t`.`TypeID` = `tt`.`ID`))) join `transactionstatus` `ts` on((`t`.`StatusID` = `ts`.`ID`))) join `bill` `b` on((`b`.`Number` = `t`.`BillToNumber`))) join `user` `u` on((`b`.`OwnerID` = `u`.`ID`))) join `bill` `bb` on((`bb`.`Number` = `t`.`BillFromNumber`))) join `user` `uu` on((`uu`.`ID` = `bb`.`OwnerID`)))  ;

-- --------------------------------------------------------

--
-- Структура для представления `v_user`
--
DROP TABLE IF EXISTS `v_user`;

DROP VIEW IF EXISTS `v_user`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_user`  AS SELECT `u`.`ID` AS `ID`, `u`.`FName` AS `FName`, `u`.`LName` AS `LName`, `u`.`MName` AS `MName`, `u`.`Login` AS `Login`, `u`.`Password` AS `Password`, `u`.`Phone` AS `Phone`, `u`.`CreationDate` AS `CreationDate`, `us`.`Name` AS `StatusName`, `r`.`Name` AS `Role` FROM ((`user` `u` join `role` `r` on((`u`.`RoleID` = `r`.`ID`))) join `userstatus` `us` on((`u`.`StatusID` = `us`.`ID`)))  ;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `bill`
--
ALTER TABLE `bill`
  ADD CONSTRAINT `bill_ibfk_1` FOREIGN KEY (`CardNumber`) REFERENCES `card` (`Number`) ON DELETE SET NULL ON UPDATE CASCADE,
  ADD CONSTRAINT `bill_ibfk_2` FOREIGN KEY (`StatusID`) REFERENCES `billstatus` (`ID`),
  ADD CONSTRAINT `bill_OwnerID_FK` FOREIGN KEY (`OwnerID`) REFERENCES `user` (`ID`);

--
-- Ограничения внешнего ключа таблицы `card`
--
ALTER TABLE `card`
  ADD CONSTRAINT `card_ibfk_1` FOREIGN KEY (`StatusID`) REFERENCES `cardstatus` (`ID`);

--
-- Ограничения внешнего ключа таблицы `transaction`
--
ALTER TABLE `transaction`
  ADD CONSTRAINT `transaction_ibfk_1` FOREIGN KEY (`TypeID`) REFERENCES `transactiontype` (`ID`) ON UPDATE CASCADE,
  ADD CONSTRAINT `transaction_ibfk_2` FOREIGN KEY (`StatusID`) REFERENCES `transactionstatus` (`ID`) ON UPDATE CASCADE,
  ADD CONSTRAINT `transaction_ibfk_3` FOREIGN KEY (`BillToNumber`) REFERENCES `bill` (`Number`) ON UPDATE CASCADE,
  ADD CONSTRAINT `transaction_ibfk_4` FOREIGN KEY (`BillFromNumber`) REFERENCES `bill` (`Number`) ON UPDATE CASCADE;

--
-- Ограничения внешнего ключа таблицы `user`
--
ALTER TABLE `user`
  ADD CONSTRAINT `user_ibfk_1` FOREIGN KEY (`StatusID`) REFERENCES `userstatus` (`ID`),
  ADD CONSTRAINT `user_ibfk_2` FOREIGN KEY (`RoleID`) REFERENCES `role` (`ID`) ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
