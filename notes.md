# Notes

## Questions
- Is Cecil generating 32 bit code?

## Packaging
- Just bring a copy of the runtime 
  + small
  + simple
  + no MS deps outside of the runtime
  - User sees folder with runtime, runtime config, dll, and script to run (this can be hidden through CLI)
  


- Wrap the entire SDK
  + More functionality
  + User sees one executable
  - Slower (needs to run both my compiler, and the f#/c# compiler to wrap the generated assembly)
  - more bloat
  - MS dependency
  - MS error messages shown to user

### Test Asts
"Prog
  [Dec
     (FunDec (Fun "test", { outtype = Ty "int"
                            arg = { argname = Val "x"
                                    type' = Ty "int" }
                            body = OpExp (IdExp (Val "x"), Plus, NumExp 99) }));
   Exp (CallExp (Fun "test", NumExp 1))]"

Result: Unit
Result: Int 100



Prog
  [Dec
     (FunDec
        (Fun "fib",
         { outtype = Ty "int"
           arg = { argname = Val "n"
                   type' = Ty "int" }
           body =
            IfExp
              (OpExp (IdExp (Val "n"), Eq, NumExp 0), NumExp 0,
               IfExp
                 (OpExp (IdExp (Val "n"), Eq, NumExp 1), NumExp 1,
                  OpExp
                    (CallExp
                       (Fun "fib", OpExp (IdExp (Val "n"), Minus, NumExp 2)),
                     Plus,
                     CallExp
                       (Fun "fib", OpExp (IdExp (Val "n"), Minus, NumExp 1))))) }));
   Exp (CallExp (Fun "fib", NumExp 10))]
Result: Unit
Result: Int 55
