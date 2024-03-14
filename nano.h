#define new(item) malloc(sizeof(item))

#define var auto
#define const const auto

#define fnc auto

#define as_str(val) (str_t){val, sizeof(val)}
typedef struct {
	char* value;
	int length;
} str_t;