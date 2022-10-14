CREATE TABLE `person` (
  `id` bigint NOT NULL AUTO_INCREMENT,
  `first_name` varchar(45) NOT NULL,
  `last_name` varchar(45) NOT NULL,
  `address` varchar(100) NOT NULL,
  `gender` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
)