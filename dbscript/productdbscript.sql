CREATE SCHEMA IF NOT EXISTS `ProductDB` DEFAULT CHARACTER SET utf8 ;
USE `productdb` ;

DROP TABLE IF EXISTS `productdb`.`category` ;

CREATE TABLE IF NOT EXISTS `productdb`.`category` (
  `CategoryId` INT NOT NULL AUTO_INCREMENT,
  `CategoryName` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`CategoryId`))
ENGINE = InnoDB DEFAULT CHARSET=utf8;


DROP TABLE IF EXISTS `productdb`.`product` ;

CREATE TABLE IF NOT EXISTS `productdb`.`product` (
  `ProductId` INT NOT NULL AUTO_INCREMENT,
  `ProductName` VARCHAR(255) NOT NULL,
  `Quantity` DOUBLE NOT NULL DEFAULT 0,
  `Unit` VARCHAR(45) NULL,
  `CategoryId` INT NOT NULL,
  PRIMARY KEY (`ProductId`),
  KEY `fk_product_category_idx` (`CategoryId`),
  CONSTRAINT `fk_product_category` FOREIGN KEY (`CategoryId`) REFERENCES `category` (`CategoryId`) ON UPDATE CASCADE
) ENGINE = InnoDB DEFAULT CHARSET=utf8;


insert into productdb.category (CategoryName) values('Food'), ('Beverage'), ('Vegetable');
insert into productdb.product(ProductName, Quantity, Unit, CategoryId) values('Rice', 10, 'bag', 1), ('Orange Juice', 20, 'bottle', 2), ('Totato', 30, 'kg', 3);


