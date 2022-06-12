import fs from 'fs';
import { join } from 'path';
import matter from 'gray-matter';

const docsDirectory = join(process.cwd(), 'docs');

export function getDocBySlug(slug) {
  const realSlug = slug.replace(/\.md$/, '');
  const fullPath = getPathFromSlug(realSlug);
  const fileContents = fs.readFileSync(fullPath, 'utf8');
  const { data, content } = matter(fileContents);

  return { slug: realSlug, meta: data, content };
}

const solutions = [
  {
    path: '2021/day1.csx',
    slug: '2021-day1.csx',
    meta: { title: 'Day 1 solution' }
  }
];

function getPathFromSlug(realSlug) {
  const solution = solutions.find((s) => s.slug === realSlug);
  if (solution) {
    return join(process.cwd(), solution.path);
  }
  return join(docsDirectory, `${realSlug}.md`);
}

export function getAllDocs() {
  const slugs = fs.readdirSync(docsDirectory);
  const docs = slugs.map((slug) => getDocBySlug(slug));

  return [...docs, ...solutions];
}
