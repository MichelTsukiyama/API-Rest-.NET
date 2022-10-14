CREATE TABLE `api_rest_udemy`.`books` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `author` VARCHAR(45) NOT NULL,
  `launch_date` DATE NOT NULL,
  `price` DECIMAL NOT NULL,
  `title` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`));