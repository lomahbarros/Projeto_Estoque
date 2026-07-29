class Produto:
    def __init__(self, codigo, nome, quantidade, preco):
        self.codigo = codigo
        self.nome = nome
        self.quantidade = quantidade
        self.preco = preco

class Estoque:
    def __init__(self):
        self.produto = {}      

    def adicionar_produto(self, produto):
        self.produto[produto.codigo] = produto

    def remover_produto(self, codigo):
        if codigo in self.produto:
            del self.produto[codigo]

    def atualizar_quantidade(self, codigo, nova_qtd):
        if codi in self.produto:
            self.produto[codigo].quantidade = nova_qtd

    def listar_produtos(self):
        for p in self.produto.values():
            print(f"{p.codigo} - {p.nome} | Qtd: {p.quantidade} | R$ {p.preco} ")         
            