using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Operatorns;

namespace Operatorns
{
	public enum Operator
	{
		Add, Subtrack, Divide, Multiply
	}
	public static class Extensions
	{
		//private static final Random PRNG = new Random();

		public static Operator randomOp(this Operator op)
		{
			return (Operator)Random.Range(0, System.Enum.GetNames(typeof(Operator)).Length);
		}

		public static string getOperatorVisual(this Operator op) {
			switch (op)
			{
				case Operator.Add:
					return "+";
				case Operator.Subtrack:
					return "-";
				case Operator.Divide:
					return "÷";
				case Operator.Multiply:
					return "x";
				default:
					return "$";
			}
		}
	}
}


public static class MathGenerator
{
	public static MathProblem GenerateProblem() {
		Operator op = Operator.Add;
		return new MathProblem(op.randomOp(), Random.Range(-5, 5));
	}
}
public class MathProblem
{
	public Operator op;
	public int number;

	public MathProblem(Operator op, int number)
	{
		this.op = op;
		this.number = number;
	}

	public string OperationVisual()
	{
		if (this.number < 0) {
			return string.Format("{0} ({1})", this.op.getOperatorVisual(), this.number);
		}
		return string.Format("{0} {1}", this.op.getOperatorVisual(), this.number);
	}
}