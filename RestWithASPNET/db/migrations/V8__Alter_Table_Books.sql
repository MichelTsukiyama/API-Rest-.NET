ALTER TABLE `api_rest_udemy`.`books` 
ADD COLUMN `enabled` BIT(1) NOT NULL DEFAULT 1 AFTER `title`;
