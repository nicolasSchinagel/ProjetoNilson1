create database dbBancoApp;
-- drop database dbBancoApp;
use dbBancoApp;
create table usuario
(
IdUsu int primary key auto_increment,
nomeUsu varchar(50) not null,
Cargo varchar(50) not null,
DataNasc datetime
);
insert into usuario(nomeUsu, Cargo, DataNasc) values('Nilson', 'Gerente', '1978/05/01'),
('Bruno', 'Colaborador', '2000/10/12');
select * from usuario;

create table EnderecoUsuario(
IdUsuEnde int primary key,
foreign key(IdUsuEnde) references usuario(IdUsu),
CEP varchar(20) not null,
Estado varchar(50) not null,
Cidade varchar(100) not null,
Bairro varchar(50) not null,
Logradouro varchar(100) not null,
Complemento varchar(50) null,
Numero int not null
);
insert into EnderecoUsuario(IdUsuEnde, CEP, Estado, Cidade, Bairro, Logradouro, Complemento, Numero) values(1, 105, 'Minas Gerais', 'Belo Horizonte', 'Santa Cecília', 'Rua das Corpes',07, 10);
insert into EnderecoUsuario(IdUsuEnde, CEP, Estado, Cidade, Bairro, Logradouro, Complemento, Numero) values(2, 288, 'Rio de Janeiro', 'Rio de Janeiro', 'Santo Amaral', 'Rua das Canarinhos', null, 90);
select * from EnderecoUsuario;



DELIMITER $$
  create procedure spCriarUsuario(
  vNomeUsu varchar(50),
  vCargo varchar(50),
  vDataNasc datetime,
  vCEP varchar(20),
  vEstado varchar(50),
  vCidade varchar(100),
  vBairro varchar(50),
  vLogradouro varchar(100),
  vComplemento varchar(50),
  vNumero int
  )
  begin
	declare vIdUsu int;
    declare vIdUsuEnde int;
    if not exists (select 1 from usuario where (nomeUsu = vNomeUsu and Cargo = vCargo and DataNasc = vDatanasc))
      then
      insert into usuario(nomeUsu, Cargo, DataNasc) values(vNomeUsu, vCargo, vDataNasc);
      select IdUsu into vIdUsu from usuario where (nomeUsu = vNomeUsu and Cargo = vCargo and DataNasc = vDataNasc);
      set vIdUsuEnde = vIdUsu;
      insert into EnderecoUsuario(IdUsuEnde, CEP, Estado, Cidade, Bairro, Logradouro, Complemento, Numero) values(vIdUsuEnde, vCEP, vEstado, vCidade, vBairro, vLogradouro , vComplemento, vNumero);
      end if;
  end;
$$
call spCriarUsuario('Alexandre pães','carpinteiro','2000/05/20', 10, 'Brasília', 'Brasília', 'Pratióticas', 'Rua do Anel vermelho', 10, 109);
call spCriarUsuario('Maria Hambugo', 'artesanalista', '1999/06/7', 6, 'São Paulo', 'Guarulhos', 'Nova festfália', 'Rua Rei Whilhn II', null, 87);
-- Nesse caso temos uma procedure para criar o usuário com o endereço dele, mas ele só pode ter um endereço e caso tenha um email idêntico ele se repete, então isso é apenas um teste para o banco, não funciona na realidade
-- Pois se quisessemos usar tudo corretamente, quase todos os atributos de endereço deveriam estar separados.


DELIMITER $$
  create procedure spAtualizarUsuario(
  vNomeUsu varchar(50),
  vCargo varchar(50),
  vDataNasc datetime,
  vCEP varchar(20),
  vEstado varchar(50),
  vCidade varchar(100),
  vBairro varchar(50),
  vLogradouro varchar(100),
  vComplemento varchar(50),
  vNumero int,
  vIdUsu int
  )
  begin
    declare vIdUsuEnde int;
    set vIdUsuEnde = vIdUsu;
    if exists (select 1 from usuario where idUsu = vIdUsu)
      then
      update usuario set nomeUsu = vNomeUsu, Cargo = vCargo, DataNasc = vDataNasc where IdUsu=vIdUsu ;
      update EnderecoUsuario set CEP = vCEP, Estado = vEstado, Cidade = vCidade, Bairro = vBairro, Logradouro = vLogradouro, Complemento = vComplemento, Numero = vNumero where IdUsuEnde = vIdUsuEnde;
      end if;
  end;
$$
call spAtualizarUsuario('Nilson', 'Estagiário', '1978/06/01', 105, 'Minas Gerais', 'Uberlândia', 'Oficina', 'Mecânicos da serra', null, 10, 1);



DELIMITER $$
  create procedure spDeletarUsuario(
  vIdUsu int
  )
  begin
    declare vIdUsuEnde int;
    set vIdUsuEnde = vIdUsu;
    if exists (select 1 from usuario where idUsu = vIdUsu)
      then
      delete from EnderecoUsuario where IdUsuEnde = vIdUsuEnde;
      delete from usuario where IdUsu=vIdUsu;
      end if;
  end;
$$
call spDeletarUsuario(1);
-- Agora, para facilitar a inserção a view lá no asp.net, usaremos uma tabela artificial, também chamada de view.
create or replace view ViewUsu as
select
u.IdUsu as "Id",
u.nomeUsu as "Nome",
u.Cargo as "Cargo",
u.DataNasc as "Data de Nascimento",
e.CEP,
e.Estado,
e.Cidade,
e.Bairro,
e.Logradouro,
e.Complemento,
e.Numero
from usuario u
inner join EnderecoUsuario e
on u.IdUsu = e.IdUsuEnde
order by u.IdUsu ASC;

select * from ViewUsu;