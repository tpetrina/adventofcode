export function Article({ children }) {
  return (
    <article className="prose lg:prose-xl px-8 m-auto my-4 sm:my-16">
      {children}
    </article>
  );
}
