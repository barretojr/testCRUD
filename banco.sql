CREATE TABLE tb_estado_civil (
  id INT PRIMARY KEY,
  descricao VARCHAR(50)
);


INSERT INTO tb_estado_civil (id, descricao)
VALUES
  (1, 'Solteiro'),
  (2, 'Casado'),
  (3, 'União estável'),
  (4, 'Divorciado'),
  (5, 'Viúvo(a)');


CREATE TABLE tb_sexo (
  id INT PRIMARY KEY,
  descricao VARCHAR(20)
);

INSERT INTO tb_sexo (id, descricao)
VALUES
  (1, 'Feminino'),
  (2, 'Masculino');


CREATE TABLE tb_pessoa (
  id INT IDENTITY(1,1) PRIMARY KEY,
  Nome VARCHAR(100) NOT NULL,
  TelefoneFixo INT,
  TelefoneCelular INT,
  Email VARCHAR(100) NOT NULL,
  SexoId INT NOT NULL,
  EstadoCivilId INT NOT NULL,
  Salario DECIMAL(10, 2),
  CONSTRAINT FK_Sexo FOREIGN KEY (SexoId) REFERENCES tb_sexo (id),
  CONSTRAINT FK_EstadoCivil FOREIGN KEY (EstadoCivilId) REFERENCES tb_estado_civil (id)
);



CREATE PROCEDURE CriarCadastroPessoa
  @id INT,
  @Nome VARCHAR(100),
  @TelefoneFixo INT,
  @TelefoneCelular INT,
  @Email VARCHAR(100),
  @SexoId INT,
  @EstadoCivilId INT,
  @Salario DECIMAL(10, 2)
AS
BEGIN
  INSERT INTO tb_pessoa (id, Nome, TelefoneFixo, TelefoneCelular, Email, SexoId, EstadoCivilId, Salario)
  VALUES (@id, @Nome, @TelefoneFixo, @TelefoneCelular, @Email, @SexoId, @EstadoCivilId, @Salario);
END


CREATE PROCEDURE EditarCadastroPessoa
  @id INT,
  @Nome VARCHAR(100),
  @TelefoneFixo INT,
  @TelefoneCelular INT,
  @Email VARCHAR(100),
  @SexoId INT,
  @EstadoCivilId INT,
  @Salario DECIMAL(10, 2)
AS
BEGIN
  UPDATE tb_pessoa
  SET Nome = @Nome,
      TelefoneFixo = @TelefoneFixo,
      TelefoneCelular = @TelefoneCelular,
      Email = @Email,
      SexoId = @SexoId,
      EstadoCivilId = @EstadoCivilId,
      Salario = @Salario
  WHERE id = @id;
END


CREATE PROCEDURE ExcluirCadastroPessoa
  @id INT
AS
BEGIN
  DELETE FROM tb_pessoa
  WHERE id = @id;
END
  

CREATE PROCEDURE MostrarCadastroPessoa
AS
BEGIN
  SELECT *
  FROM tb_pessoa;
END

  
CREATE PROCEDURE MostrarCadastroPessoaId
    @id INT
AS
BEGIN
    SELECT *
    FROM tb_pessoa
    WHERE id = @id;
END
