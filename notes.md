# Notes

- Goal = support function recursion

Prog
  [Dec
     (FunDec
        (Fun "fact",
         { name = Fun "f"
           outtype = Ty "int"
           arg = { argname = Val "n"
                   type' = Ty "int" }
           body =
            OpExp
              (IfExp
                 (OpExp (IdExp (Val "n"), Eq, NumExp 0), NumExp 1,
                  IdExp (Val "n")), Times,
               CallExp (Fun "f", OpExp (IdExp (Val "n"), Minus, NumExp 1))) }));
   Exp (CallExp (Fun "fact", NumExp 10))]

- how do we not get "function not found" when executing the body?
    - we need the function to be entered into the env by the time it gets to the recursive call


