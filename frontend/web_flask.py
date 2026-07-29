from flask import Flask, request, render_template
from backend.services import cadastrar_produto, listar_produtos

app = Flask(__name__, template_folder="templates")

@app.route("/")
def home():
    produtos = listar_produtos() or []  # garante que nunca seja None
    return render_template("index.html", produtos=produtos)

@app.route("/add", methods=["POST"])
def add():
    codigo = request.form["codigo"]
    nome = request.form["nome"]
    quantidade = int(request.form["quantidade"])
    preco = float(request.form["preco"])
    cadastrar_produto(codigo, nome, quantidade, preco)
    return home()

if __name__ == "__main__":
    app.run(debug=True)
