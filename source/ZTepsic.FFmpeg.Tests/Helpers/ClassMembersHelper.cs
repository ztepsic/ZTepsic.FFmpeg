using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ZTepsic.FFmpeg.Tests {

	/// <summary>
	/// Helper static class that provides methods for setting and getting values from class members
	/// (fields and properties) regardless of the visibility.
	/// </summary>
	public static class ClassMembersHelper {

		/// <summary>
		/// Get PeorpertyInfo object
		/// 
		/// Based on static reflection: 
		/// http://jagregory.com/writings/introduction-to-static-reflection/
		/// http://ayende.com/blog/779/static-reflection
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static PropertyInfo GetPropertyOf<T>(Expression<Func<T, object>> expression) {
			var memberExpression = expression.Body as MemberExpression;

			if (memberExpression == null) {
				throw new InvalidOperationException("Not a member access.");
			}

			return memberExpression.Member as PropertyInfo;
		}

		public static PropertyInfo GetPropertyOf<T>(Expression<Func<T, decimal>> expression) {
			var memberExpression = expression.Body as MemberExpression;

			if (memberExpression == null) {
				throw new InvalidOperationException("Not a member access.");
			}

			return memberExpression.Member as PropertyInfo;
		}

		public static PropertyInfo GetPropertyOf<T>(Expression<Func<T, double>> expression) {
			var memberExpression = expression.Body as MemberExpression;

			if (memberExpression == null) {
				throw new InvalidOperationException("Not a member access.");
			}

			return memberExpression.Member as PropertyInfo;
		}

		public static PropertyInfo GetPropertyOf<T>(Expression<Func<T, float>> expression) {
			var memberExpression = expression.Body as MemberExpression;

			if (memberExpression == null) {
				throw new InvalidOperationException("Not a member access.");
			}

			return memberExpression.Member as PropertyInfo;
		}

		public static PropertyInfo GetPropertyOf<T>(Expression<Func<T, int>> expression) {
			var memberExpression = expression.Body as MemberExpression;

			if (memberExpression == null) {
				throw new InvalidOperationException("Not a member access.");
			}

			return memberExpression.Member as PropertyInfo;
		}
		public static PropertyInfo GetPropertyOf<T>(Expression<Func<T, short>> expression) {
			var memberExpression = expression.Body as MemberExpression;

			if (memberExpression == null) {
				throw new InvalidOperationException("Not a member access.");
			}

			return memberExpression.Member as PropertyInfo;
		}

		public static PropertyInfo GetPropertyOf<T>(Expression<Func<T, bool>> expression) {
			var memberExpression = expression.Body as MemberExpression;

			if (memberExpression == null) {
				throw new InvalidOperationException("Not a member access.");
			}

			return memberExpression.Member as PropertyInfo;
		}

		public static void SetPropertyOf<TObject>(object obj, Expression<Func<TObject, object>> expression, object value) {
			PropertyInfo property = GetPropertyOf(expression);

			property.SetValue(obj, value, null);
		}

		public static void SetPropertyOf<TObject>(object obj, Expression<Func<TObject, decimal>> expression, object value) {
			PropertyInfo property = GetPropertyOf(expression);

			property.SetValue(obj, value, null);
		}

		public static void SetPropertyOf<TObject>(object obj, Expression<Func<TObject, double>> expression, object value) {
			PropertyInfo property = GetPropertyOf(expression);

			property.SetValue(obj, value, null);
		}

		public static void SetPropertyOf<TObject>(object obj, Expression<Func<TObject, float>> expression, object value) {
			PropertyInfo property = GetPropertyOf(expression);

			property.SetValue(obj, value, null);
		}

		public static void SetPropertyOf<TObject>(object obj, Expression<Func<TObject, int>> expression, object value) {
			PropertyInfo property = GetPropertyOf(expression);

			property.SetValue(obj, value, null);
		}

		public static void SetPropertyOf<TObject>(object obj, Expression<Func<TObject, short>> expression, object value) {
			PropertyInfo property = GetPropertyOf(expression);

			property.SetValue(obj, value, null);
		}

		public static void SetPropertyOf<TObject>(object obj, Expression<Func<TObject, bool>> expression, object value) {
			PropertyInfo property = GetPropertyOf(expression);

			property.SetValue(obj, value, null);
		}

		public static Object SetPropertyTo<TObject>(this object obj, Expression<Func<TObject, object>> expression, object value) {
			SetPropertyOf(obj, expression, value);
			return obj;
		}

		public static Object SetPropertyTo<TObject>(this object obj, Expression<Func<TObject, decimal>> expression, object value) {
			SetPropertyOf(obj, expression, value);
			return obj;
		}

		public static Object SetPropertyTo<TObject>(this object obj, Expression<Func<TObject, double>> expression, object value) {
			SetPropertyOf(obj, expression, value);
			return obj;
		}

		public static Object SetPropertyTo<TObject>(this object obj, Expression<Func<TObject, float>> expression, object value) {
			SetPropertyOf(obj, expression, value);
			return obj;
		}

		public static Object SetPropertyTo<TObject>(this object obj, Expression<Func<TObject, int>> expression, object value) {
			SetPropertyOf(obj, expression, value);
			return obj;
		}

		public static Object SetPropertyTo<TObject>(this object obj, Expression<Func<TObject, short>> expression, object value) {
			SetPropertyOf(obj, expression, value);
			return obj;
		}

		public static Object SetPropertyTo<TObject>(this object obj, Expression<Func<TObject, bool>> expression, object value) {
			SetPropertyOf(obj, expression, value);
			return obj;
		}

	}
}
