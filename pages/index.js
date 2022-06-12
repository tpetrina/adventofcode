import { Article } from '~/components/Article';
import { Layout } from '~/components/Layout';
import { getAllDocs } from '~/lib/docs';

export default function IndexPage({ docs }) {
  const meta = {};

  return (
    <Layout meta={meta}>
      <Article>
        <p>Hello world</p>
        {docs.map((doc) => (
          <li>
            <pre>{JSON.stringify(doc, null, 2)}</pre>
          </li>
        ))}
      </Article>
    </Layout>
  );
}

export async function getStaticProps() {
  const docs = getAllDocs();

  return {
    props: {
      docs
    }
  };
}
