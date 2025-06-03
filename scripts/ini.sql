DROP TABLE IF EXISTS `users`;

CREATE TABLE users (
   Id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
   Nome VARCHAR(255) NOT NULL,
   CPF VARCHAR(11) NOT NULL,
   Email VARCHAR(255) NOT NULL UNIQUE 
);

DROP TABLE IF EXISTS `orders`;
CREATE TABLE `orders` (
    Id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    OrderCode VARCHAR(255) NOT NULL UNIQUE,
    UserId BIGINT UNSIGNED NULL,
    TotalPrice DECIMAL(18, 2) NOT NULL,
    Status ENUM('Created', 'Received', 'InPreparation', 'Ready', 'Finished') NOT NULL,
    Expiration DATETIME NOT NULL,
    FOREIGN KEY (UserId) REFERENCES users(Id)
);

DROP TABLE IF EXISTS `products`;
CREATE TABLE `products` (
    Id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Description TEXT,
    Price DECIMAL(10, 2) NOT NULL,
    Quantity INT UNSIGNED NOT NULL DEFAULT 0,
    Type ENUM('Burger', 'Beverage', 'Dessert') NOT NULL
);

DROP TABLE IF EXISTS `order_items`;
CREATE TABLE `order_items` (
    Id BIGINT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    OrderId BIGINT UNSIGNED NULL,
    ProductId BIGINT UNSIGNED NULL,
    FOREIGN KEY (OrderId) REFERENCES orders(Id),
    FOREIGN KEY (ProductId) REFERENCES products(Id)
);

INSERT INTO products (Name, Description, Price, Quantity, Type) VALUES
('Cheeseburger Clássico', 'Hambúrguer de carne, queijo, alface, tomate, picles e molho especial.', 25.99, 100, 'Burger'),
('X-Bacon', 'Hambúrguer de carne, bacon crocante, queijo, alface e maionese.', 28.50, 80, 'Burger'),
('Hamburguer Vegano', 'Hambúrguer de grão de bico, queijo vegano, rúcula e tomate seco.', 32.00, 50, 'Burger'),
('Chicken Crispy', 'Filé de frango empanado, queijo, alface americana e molho honey mustard.', 27.00, 75, 'Burger'),
('Duplo Cheddar', 'Dois hambúrgueres de carne, muito cheddar e cebola caramelizada.', 35.90, 60, 'Burger');

INSERT INTO products (Name, Description, Price, Quantity, Type) VALUES
('Coca-Cola Lata', 'Refrigerante Coca-Cola em lata 350ml.', 6.00, 200, 'Beverage'),
('Suco de Laranja Natural', 'Suco de laranja espremido na hora, 300ml.', 10.50, 120, 'Beverage'),
('Água Mineral', 'Garrafa de água mineral sem gás 500ml.', 4.00, 150, 'Beverage'),
('Cerveja Artesanal IPA', 'Cerveja tipo IPA, 500ml.', 22.00, 40, 'Beverage'),
('Milkshake Chocolate', 'Milkshake cremoso de chocolate com chantilly.', 18.00, 90, 'Beverage');

INSERT INTO products (Name, Description, Price, Quantity, Type) VALUES
('Petit Gateau', 'Bolo de chocolate com recheio cremoso e sorvete de creme.', 20.00, 50, 'Dessert'),
('Brownie com Sorvete', 'Brownie quentinho com uma bola de sorvete de baunilha.', 18.50, 60, 'Dessert'),
('Mousse de Maracujá', 'Sobremesa leve e refrescante de maracujá.', 15.00, 70, 'Dessert'),
('Pudim de Leite', 'Clássico pudim de leite condensado.', 16.00, 45, 'Dessert'),
('Torta Holandesa', 'Fatia de torta holandesa.', 19.50, 55, 'Dessert');