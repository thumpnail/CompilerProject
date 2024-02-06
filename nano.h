#define new(item) malloc(sizeof(item))

#define let auto
#define const const auto

#define fnc auto

#define str(val) {val, sizeof(val)}
typedef struct {
	char* value;
	int length;
}str;