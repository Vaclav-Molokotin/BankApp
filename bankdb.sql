-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Хост: 127.0.0.1:3306
-- Время создания: Янв 10 2024 г., 16:37
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
(3, 'Закрыть'),
(1, 'Заморожен');

-- --------------------------------------------------------

--
-- Структура таблицы `card`
--

DROP TABLE IF EXISTS `card`;
CREATE TABLE IF NOT EXISTS `card` (
  `Number` char(16) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Номер карты',
  `CVC` int UNSIGNED NOT NULL COMMENT 'CVC-Код',
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
('0247143101458667', 803, '2025-01-10', '2024-01-10', 1),
('0733041261344240', 602, '2025-01-10', '2024-01-10', 1),
('1517374331372710', 395, '2025-01-10', '2024-01-10', 3),
('1716628744254466', 381, '2025-01-10', '2024-01-10', 3),
('2233210544248546', 865, '2025-01-09', '2024-01-09', 1),
('3026744846176686', 795, '2025-01-10', '2024-01-10', 3),
('3125201034610578', 366, '2025-01-10', '2024-01-10', 3),
('3664603715283542', 5, '2025-01-10', '2024-01-10', 1),
('3717275070887146', 504, '2025-01-10', '2024-01-10', 3),
('4142172110345617', 691, '2025-01-10', '2024-01-10', 3),
('4158620733608754', 932, '2025-01-10', '2024-01-10', 3),
('4267367530136106', 195, '2025-01-10', '2024-01-10', 3),
('4570837081408483', 800, '2025-01-10', '2024-01-10', 1),
('5124125355566307', 640, '2025-01-09', '2024-01-09', 3),
('5206603233686013', 135, '2025-01-10', '2024-01-10', 3),
('6373770355756415', 812, '2025-01-10', '2024-01-10', 3),
('6377078748708380', 495, '2025-01-10', '2024-01-10', 3),
('7431261028847230', 254, '2025-01-09', '2024-01-09', 1),
('7502658188623832', 657, '2025-01-10', '2024-01-10', 3),
('7862525787811538', 507, '2025-01-10', '2024-01-10', 3),
('8452726416258427', 741, '2025-01-10', '2024-01-10', 3);

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
  `TransactionTypeID` int NOT NULL COMMENT 'Тип транзакции',
  `BillToNumber` char(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Счёт получателя',
  `BillFromNumber` char(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT 'Счёт отправителя',
  `StatusID` int NOT NULL COMMENT 'Статус',
  `Amount` float UNSIGNED NOT NULL COMMENT 'Сумма перевода',
  `Date` date NOT NULL COMMENT 'Дата перевода',
  PRIMARY KEY (`ID`),
  KEY `TransactionTypeID` (`TransactionTypeID`),
  KEY `StatusID` (`StatusID`),
  KEY `BillToNumber` (`BillToNumber`),
  KEY `BillFromNumber` (`BillFromNumber`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
(1, 'Delivered'),
(3, 'Rejected'),
(2, 'Waiting');

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
(3, 'Refill'),
(1, 'Transfer'),
(2, 'TransferBetweenBills'),
(4, 'Withdrawal');

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
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='Таблица пользователей';

--
-- Дамп данных таблицы `user`
--

INSERT INTO `user` (`ID`, `FName`, `LName`, `MName`, `Login`, `Password`, `Phone`, `RoleID`, `StatusID`, `CreationDate`) VALUES
(1, 'Евгений', 'Прохоров', 'Павлович', 'Ev_Pav', '26fe0cdfe99bfa306e31733c4e2b17dc', NULL, 3, 1, '0000-00-00 00:00:00'),
(2, 'Екатерина', 'Лебедева', 'Никитична', 'Eka_Leb', '26fe0cdfe99bfa306e31733c4e2b17dc', '4294967295', 3, 1, '0000-00-00 00:00:00'),
(3, 'Антонио', 'Попов', 'Робертович', 'Anto_Pop', '202cb962ac59075b964b07152d234b70', '+7-935-245-28-34', 3, 1, '0000-00-00 00:00:00');

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
`Balance` float unsigned
,`CardNumber` char(16)
,`Name` varchar(50)
,`Number` char(20)
,`Owner` varchar(50)
,`StatusID` int
);

-- --------------------------------------------------------

--
-- Дублирующая структура для представления `v_transaction`
-- (См. Ниже фактическое представление)
--
DROP VIEW IF EXISTS `v_transaction`;
CREATE TABLE IF NOT EXISTS `v_transaction` (
`Amount` float unsigned
,`BillFromNumber` char(20)
,`BillToNumber` char(20)
,`Date` date
,`ID` int unsigned
,`Status` varchar(50)
,`type` varchar(50)
);

-- --------------------------------------------------------

--
-- Дублирующая структура для представления `v_user`
-- (См. Ниже фактическое представление)
--
DROP VIEW IF EXISTS `v_user`;
CREATE TABLE IF NOT EXISTS `v_user` (
`CreationDate` datetime
,`FName` varchar(75)
,`ID` int unsigned
,`LName` varchar(75)
,`Login` varchar(50)
,`MName` varchar(75)
,`Password` varchar(32)
,`Phone` char(16)
,`Role` varchar(100)
,`StatusName` varchar(75)
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
-- Структура для представления `v_transaction`
--
DROP TABLE IF EXISTS `v_transaction`;

DROP VIEW IF EXISTS `v_transaction`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `v_transaction`  AS SELECT `t`.`ID` AS `ID`, `t`.`BillFromNumber` AS `BillFromNumber`, `t`.`BillToNumber` AS `BillToNumber`, `t`.`Amount` AS `Amount`, `tt`.`Name` AS `type`, `t`.`Date` AS `Date`, `ts`.`Name` AS `Status` FROM ((`transaction` `t` join `transactiontype` `tt` on((`t`.`TransactionTypeID` = `tt`.`ID`))) join `transactionstatus` `ts` on((`t`.`StatusID` = `ts`.`ID`)))  ;

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
  ADD CONSTRAINT `transaction_ibfk_1` FOREIGN KEY (`TransactionTypeID`) REFERENCES `transactiontype` (`ID`) ON UPDATE CASCADE,
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
