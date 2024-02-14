#include "mathexpr.hpp"

#include <exception>
#include <iostream>

extern "C" {

__declspec(dllexport) int process_mathexpr(char const *expr, double *value) {
    try {
        *value = math::process_mathexpr(expr);
        return 0;
    } catch (std::exception const &ex) {
        std::cerr << ex.what() << std::endl;
    }
    return 1;
}

}
