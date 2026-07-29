from backend.models import Produto, Estoque

estoque = Estoque()

def cadastrar_produto(codigo, nome, quantidade, preco):
    produto = Produto(codigo, nome, quantidade, preco)
    estoque.adicionar_produto(produto)

def listar_produtos():
    return estoque.listar_produtos()  # sempre retorna lista
