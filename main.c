#include <stdio.h>
#include <stdlib.h>

#define new(item) malloc(sizeof(item))
#define Token int

enum {
    TOKEN_IF = 0,
    TOKEN_FOR = 1
};

typedef struct t_ParserContext {
    Token token;
} ParserContext;

int ParseStatement(ParserContext*);
int ParseIfStatement(ParserContext*);
int ParseForStatement(ParserContext*);

int main() {
    ParserContext* ctx = new(ParserContext);
    ParseStatement(ctx);
}

int ParseStatement(ParserContext* ctx) {
    switch (ctx->token) {
    case TOKEN_IF:
        ParseIfStatement(ctx);
        break;
    case TOKEN_FOR:
        ParseForStatement(ctx);
        break;
    }
    return 0;
}

int ParseIfStatement(ParserContext* ctx) {
    //ctx.peek("if");
    //ParseExpression(ctx);
    //ctx.peek("{");
    ParseStatement(ctx);
    //ctx.peek("}");
    return 0;
}
int ParseForStatement(ParserContext* ctx) {
    //ctx.peek("if");
    //ParseExpression(ctx);
    //...
    //ctx.peek("{");
    ParseStatement(ctx);
    //ctx.peek("}");
    return 0;
}