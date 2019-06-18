-- ENCHER A TABELA CLIENTES --
insert into ServEntr.Cliente(nr_reg, nome, nif, email, nr_tel, morada) values (1, 'Alberto Santos', 187293729, 'albsantos@hotmail.com', 910926472, 'Rua de Adriano Serra 143, 3800-009 Aveiro');
insert into ServEntr.Cliente(nr_reg, nome, nif, email, nr_tel, morada) values (2, 'Adriano Alves', 654812350, 'aalves@hotmail.com', 915846235, 'Rua do Carmo 35, 3804-503 Aveiro');
insert into ServEntr.Cliente(nr_reg, nome, nif, email, nr_tel, morada) values (5, 'Ana Rita', 858412350, 'anarita@gmail.com', 936420805, 'Rua Agostinho da Silva Rocha 96, 4479-003 Maia');

-- ENCHER A TABELA MOTORISTAS --
insert into ServEntr.Motorista(nr_reg, nome, nif, email, nr_tel, matricula, marcaveiculo, morada) values (5, 'Dany Costa', 845461227, 'danycosta@gmail.com', 934656115, '50-47-KL', 'Mercedes-Benz', 'Rua do Tony 127, 4150-069 Porto');
insert into ServEntr.Motorista(nr_reg, nome, nif, email, nr_tel, matricula, marcaveiculo, morada) values (4, 'Rita Martins', 354268559, 'ritamartins@outlook.com', 961542278, 'UJ-87-46', 'Renault', 'Avenida Descobrimentos 498, 4404-503 Vila Nova de Gaia');
insert into ServEntr.Motorista(nr_reg, nome, nif, email, nr_tel, matricula, marcaveiculo, morada) values (6, 'Osvaldo Carolino', 254811089, 'osvcarol@sapo.pt', 913452189, '20-CN-54', 'BMW', 'Rua António Moreira da Silva 17, 4479-002 Maia');

-- ENCHER A TABELA ESTABELECIMENTOS --
insert into ServEntr.Estabelecimento(nome, tipo, morada, horario) values ('McDonalds', 'Fast Food', 'Praça da Liberdade 126, 4000-322 Porto', 'segunda-feira	08:00–02:00,terça-feira	08:00–02:00, quarta-feira 08:00–02:00, quinta-feira	08:00–02:00, sexta-feira 08:00–03:00, sábado 08:00–03:00, domingo 08:00–02:00');
insert into ServEntr.Estabelecimento(nome, tipo, morada, horario) values ('PizzaHut', 'Pizzaria', 'Av. do Brasil 561, 4150-153 Porto', 'segunda-feira 12:00–23:00,terça-feira 12:00–23:00,quarta-feira 12:00–23:00,quinta-feira	12:00–23:00,sexta-feira	12:00–00:00,sábado 12:00–00:00,domingo 12:00–23:00');

-- ENCHER A TABELA PRODUTOS --
insert into ServEntr.Produto (id, nome, preco, descricao, nome_estab) values ('P-4bh', 'Big Mac', 5.50, 'A sanduíche dupla mais cobiçada no mundo inteiro. Feita com dois suculentos hambúrgueres 100% carne de vaca, queijo fundido, pepino, cebola, alface e um molho irresistível. Uma combinação única.', 'McDonalds');
insert into ServEntr.Produto (id, nome, preco, descricao, nome_estab) values ('P-ad6', 'Cheeseburger', 1.30, 'O nosso clássico cheeseburger com 100% carne de vaca temperada com uma pitada de sal e pimenta, queijo cheddar, pickles, cebola ketchup e mostarda.', 'McDonalds');
insert into ServEntr.Produto (id, nome, preco, descricao, nome_estab) values ('P-9jk', 'Double Cheeseburger', 2.50, 'Melhor que um hambúrguer 100% carne de vaca, só dois hambúrgueres 100% carne de vaca. E melhor que uma fatia de queijo derretido, só duas fatias de queijo derretido em cima de dois hambúrgueres 100% carne de vaca. Genuinamente: é do melhor.', 'McDonalds');
insert into ServEntr.Produto (id, nome, preco, descricao, nome_estab) values ('P-d55', 'Salada Campestre', 3.50, 'Receita clássica com frango, bacon e tomate cherry.', 'McDonalds');

-- ENCHER A TABELA AVALIAÇÕES --
insert into ServEntr.Avaliacao(id, obs, estrelas_compra, estrelas_motorista, tempo_espera) values (1, null, 5, 5, 10);
insert into ServEntr.Avaliacao(id, obs, estrelas_compra, estrelas_motorista, tempo_espera) values (2, null, 5, 4, 13);
insert into ServEntr.Avaliacao(id, obs, estrelas_compra, estrelas_motorista, tempo_espera) values (3, null, 4, 3, 15);
insert into ServEntr.Avaliacao(id, obs, estrelas_compra, estrelas_motorista, tempo_espera) values (4, null, 5, 5, 6);

-- ENCHER A TABELA PAGAMENTOS --
insert into ServEntr.Pagamento(preco, morada_fat, metodo, referencia) values (6.50, 'Rua do Carmo 35, 3804-503 Aveiro', 'PayPal', 123456);
insert into ServEntr.Pagamento(preco, morada_fat, metodo, referencia) values (13.60, 'Rua Agostinho da Silva Rocha 96, 4479-003 Maia', 'PayPal', 654321);

-- ENCHER A TABELA ENCOMENDA --
insert into ServEntr.Encomenda (id, morada, obs, preco_total, ref_pagamento, nr_reg_cliente, nr_reg_motorista, id_avaliacao, status) values ('E-1u3', 'Rua do Carmo 35, 3804-503 Aveiro', null, 6.50, 123456, 2, 6, 1, 'ativa');
insert into ServEntr.Encomenda (id, morada, obs, preco_total, ref_pagamento, nr_reg_cliente, nr_reg_motorista, id_avaliacao, status) values ('E-k74', 'Rua Agostinho da Silva Rocha 96, 4479-003 Maia', null, 13.60, 654321, 5, 4, 2, 'terminada');

-- ENCHER A TABELA CONSTITUIDA POR ENCOMENDAS E PRODUTOS --
insert into ServEntr.EncomendaProduto(id_encomenda, id_produto) values ('E-1u3', 'P-4bh');
insert into ServEntr.EncomendaProduto(id_encomenda, id_produto) values ('E-1u3', 'P-ad6');
insert into ServEntr.EncomendaProduto(id_encomenda, id_produto) values ('E-k74', 'P-etf');
insert into ServEntr.EncomendaProduto(id_encomenda, id_produto) values ('E-k74', 'P-t65');
insert into ServEntr.EncomendaProduto(id_encomenda, id_produto) values ('E-k74', 'P-10p');
insert into ServEntr.EncomendaProduto(id_encomenda, id_produto) values ('E-k74', 'P-520');
